using System.Drawing;
using System.Windows.Forms;

namespace Custom_UEVR_Injector
{
    public static class Slides
    {
        public static SliderGroup ScreenPercentage { get; private set; }
        public static SliderGroup ResolutionQuality { get; private set; }
        public static SliderGroup ViewDistanceQuality { get; private set; }
        public static SliderGroup AntiAliasingQuality { get; private set; }
        public static SliderGroup PostProcessQuality { get; private set; }
        public static SliderGroup ShadowQuality { get; private set; }
        public static SliderGroup TextureQuality { get; private set; }
        public static SliderGroup EffectsQuality { get; private set; }
        public static SliderGroup FoliageQuality { get; private set; }
        public static SliderGroup ShadingQuality { get; private set; }
        public static SliderGroup ReflectionQuality { get; private set; }
        public static SliderGroup VSync { get; private set; }
        public static SliderGroup VolumetricCloud { get; private set; }
        public static SliderGroup GlobalIlluminationQuality { get; private set; }
        public static SliderGroup ReflectionMethod { get; private set; }

        public static void Initialize(Form parentForm)
        {
            int s_posX = 540;
            int s_posY = 10;
            int s_height = 30;

            ScreenPercentage = new SliderGroup(
                "Screen Percentage:",
                new Point(s_posX, line_height(s_posY, s_height, 1)),
                200,
                30,
                150,
                100,
                false,
                "<30-150%>"
            );
            ScreenPercentage.AddToForm(parentForm);

            ResolutionQuality = new SliderGroup(
                "Resolution Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 2)),
                200,
                10,
                100,
                100,
                false,
                "<10-100%>"
            );
            ResolutionQuality.AddToForm(parentForm);

            ViewDistanceQuality = new SliderGroup(
                "View Distance Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 3)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            ViewDistanceQuality.AddToForm(parentForm);

            AntiAliasingQuality = new SliderGroup(
                "Anti-Aliasing Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 4)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            AntiAliasingQuality.AddToForm(parentForm);

            PostProcessQuality = new SliderGroup(
                "Post Process Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 5)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            PostProcessQuality.AddToForm(parentForm);

            ShadowQuality = new SliderGroup(
                "Shadow Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 6)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            ShadowQuality.AddToForm(parentForm);

            TextureQuality = new SliderGroup(
                "Texture Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 7)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            TextureQuality.AddToForm(parentForm);

            EffectsQuality = new SliderGroup(
                "Effects Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 8)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            EffectsQuality.AddToForm(parentForm);

            FoliageQuality = new SliderGroup(
                "Foliage Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 9)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            FoliageQuality.AddToForm(parentForm);

            ShadingQuality = new SliderGroup(
                "Shading Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 10)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            ShadingQuality.AddToForm(parentForm);

            ReflectionQuality = new SliderGroup(
                "Reflection Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 11)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            ReflectionQuality.AddToForm(parentForm);

            VSync = new SliderGroup(
                "VSync:",
                new Point(s_posX, line_height(s_posY, s_height, 12)),
                200,
                0,
                1,
                1,
                true,
                "<0=Off,1=On>"
            );
            VSync.AddToForm(parentForm);

            VolumetricCloud = new SliderGroup(
                "Volumetric Cloud:",
                new Point(s_posX, line_height(s_posY, s_height, 13)),
                200,
                0,
                1,
                0,
                true,
                "<0=Off,1=On>"
            );
            VolumetricCloud.AddToForm(parentForm);

            GlobalIlluminationQuality = new SliderGroup(
                "Global Illumination Quality:",
                new Point(s_posX, line_height(s_posY, s_height, 14)),
                200,
                0,
                3,
                2,
                true,
                "<0=Low,1=Medium,2=High,3=Epic>"
            );
            GlobalIlluminationQuality.AddToForm(parentForm);

            ReflectionMethod = new SliderGroup(
                "Reflection Method:",
                new Point(s_posX, line_height(s_posY, s_height, 15)),
                200,
                0,
                2,
                0,
                true,
                "<0=SSR,1=Lumen,2=Ray Tracing>"
            );
            ReflectionMethod.AddToForm(parentForm);
        }

        public static int line_height(int pos_y, int s_height, int num)
        {
            return pos_y + (s_height * num);
        }
    }
}
