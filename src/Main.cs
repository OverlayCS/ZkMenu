using System;
using System.Collections.Generic;
using System.Text;
using ZkMenu.src.Patching;

namespace ZkMenu.src
{
    public class Main
    {
        public static void start()
        {
            //called by loader
        }


        public static void enabled()
        {
            //called by loader
            Patcher.Patch();
        }

        public static void disabled()
        {
            //called by loader
            Patcher.Unpatch();
        }
        public static void awake()
        {
            //called by loader
        }

        public static void update()
        {
            //called by loader
        }
    }
}
