using HarmonyLib;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannedNet.Client.Patches
{
    internal class ImageFix
    {
        [HarmonyPatch(typeof(OHDHPENHDAP), "DHEFMCDHLCG")]
        internal class ImageSigPatch
        {
            // if i miss this jumpshot ill kill myself
            private static void Postfix(ref bool __result)
            {
                __result = true;
            }
        }
    }
}
