using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace VistaGlass
{
    public class VistaGlass
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            public int cxLeftWidth;      // width of left border that retains its size
            public int cxRightWidth;     // width of right border that retains its size
            public int cyTopHeight;      // height of top border that retains its size
            public int cyBottomHeight;   // height of bottom border that retains its size
        };

        public static void MakeGlass(Window window)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(window);
            IntPtr myHwnd = windowInteropHelper.Handle;
            HwndSource mainWindowSrc = System.Windows.Interop.HwndSource.FromHwnd(myHwnd);

            mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

            MARGINS margins = new MARGINS()
            {
                cxLeftWidth = -1,
                cxRightWidth = -1,
                cyBottomHeight = -1,
                cyTopHeight = -1
            };

            DwmExtendFrameIntoClientArea(myHwnd, ref margins);
        }

        public static bool IsSupported()
        {
            bool isGlassSupported = false;
            DwmIsCompositionEnabled(ref isGlassSupported);
            return isGlassSupported;
        }

        [DllImport("DwmApi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        private static extern void DwmIsCompositionEnabled(ref bool pfEnabled);
    }
}
