using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Medli.SysInternal;

namespace Medli
{
    /// <summary>
    /// Will hold the environment methods which will be called by various components in Medli
    /// </summary>
    public class MEnvironment
    {
        public static string dir_ext = @"\";
        /// <summary>
        /// Small and simple method saves typing this method out over and over again - only has to be called once
        /// </summary>
        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        /// <summary>
        /// Sets the filesystems current directory to its initial value
        /// i.e. the root of the storage device, same initial value but keeps them separate
        /// </summary>
        public static string current_dir = @"0:\";
        /// <summary>
        /// Defines the root directory's value, same as current_dir's initial value but keeps them separate
        /// </summary>
        public static string root_dir = @"0:\";

        public static string rootpass = "default";
        public static string rootpass_rockpotato = AIC_Framework.Crypto.MD5.hash(rootpass);
        public static string rpf = KernelVariables.rootdir + "rootpass.sys";

        public static string usrpass = "default";
        public static string usrpass_rockpotato = AIC_Framework.Crypto.MD5.hash(usrpass);
        public static string upf = KernelVariables.homedir + KernelVariables.username + @"\pass.sys";

        public static void WriteUserPass()
        {
            File.WriteAllText(upf, usrpass_rockpotato);
        }
        public static void WriteRootPass()
        {
            File.WriteAllText(rpf, rootpass_rockpotato);
        }
        public static string current_usr_dir = KernelVariables.homedir + KernelVariables.username;
    }
}
