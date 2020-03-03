using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Transitions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        PixelShaderEffect DissolveEffect;
        ICanvasImage DissolveMask;
        ICanvasImage CanvasSourceImageLarge1;
        ICanvasImage CanvasSourceImageLarge2;
        ICanvasImage CanvasSourceImageSmall1;
        ICanvasImage CanvasSourceImageSmall2;
        Vector2 CanvasPosition = new Vector2(640, 369);



        private void canvas_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(canvas_CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task canvas_CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            DissolveEffect = new PixelShaderEffect(await ReadAllBytes("Assets/Shaders/WipeUp.bin"));
            //DissolveEffect.Properties["feather"] = 0.1f;
            DissolveMask = await CreateRippleEffect();
            CanvasSourceImageLarge1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///" + "Assets/Images/beautiful-bloom-blooming-blossom-414083.jpg"));
            CanvasSourceImageLarge2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///" + "Assets/Images/nature-red-forest-leaves-33109.jpg"));
            CanvasSourceImageSmall1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///" + "Assets/Images/beach-birds-calm-clouds-219998.jpg"));
            CanvasSourceImageSmall2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///" + "Assets/Images/photography-of-trees-covered-with-snow-773594.jpg"));
            try
            {
                var info = Windows.Graphics.Display.DisplayInformation.GetForCurrentView();
                DissolveEffect.Properties["dpi"] = info.LogicalDpi;
                DissolveEffect.Properties["height"] = 369f;
                //DissolveEffect.Properties["progress"] = (float)ProgressSlider.Value;
                DissolveEffect.Source1 = CanvasSourceImageSmall1;
                //DissolveEffect.Source2 = CanvasSourceImageSmall2;
            }
            catch { }

        }

        async Task<PixelShaderEffect> CreateRippleEffect()
        {
            var ripplesPixelShaderEffect = new PixelShaderEffect(await ReadAllBytes("Assets/Shaders/Ripples.bin"));
            ripplesPixelShaderEffect.Properties["frequency"] = 0.05f;
            ripplesPixelShaderEffect.Properties["dpi"] = 96f;
            ripplesPixelShaderEffect.Properties["offset"] = 0f;
            ripplesPixelShaderEffect.Properties["center"] = new Vector2(1280f / 2f, 853 / 2f);
            return ripplesPixelShaderEffect;
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            //args.DrawingSession.DrawText("Hello, World!", 100, 100, Colors.Green);
            args.DrawingSession.DrawImage(DissolveEffect);
        }

        static async Task<byte[]> ReadAllBytes(string filename)
        {
            var uri = new Uri("ms-appx:///" + filename);
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            Windows.Storage.Streams.IBuffer buffer = await FileIO.ReadBufferAsync(file);

            return buffer.ToArray();
        }

        private void ProgressSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (DissolveEffect != null)
            {
               DissolveEffect.Properties["progress"] = (float)ProgressSlider.Value;

            }
        }
        private void FeatherSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (DissolveEffect != null)
            {
                DissolveEffect.Properties["feather"] = (float)FeatherSlider.Value;

            }
        }
        private void canvas_Draw_1(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {

        }

        private void canvas_CreateResources_1(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {

        }

        private void canvas_Holding(object sender, HoldingRoutedEventArgs e)
        {

        }


    }
}
