using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
            PressAnyKey("Press any key to continue...");
        }
        public static void PressAnyKey(string text)
        {
            Console.WriteLine(text);
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

        public static string rootpass;
        public static string rootpass_md5; //= AIC_Framework.Crypto.MD5.hash(rootpass);
        public static string usrpass;
        public static string usrpass_md5; //= AIC_Framework.Crypto.MD5.hash(usrpass);

        public static string rpf = KernelVariables.rootdir + "pass.sys";
        public static string upf = KernelVariables.homedir + KernelVariables.username + "pass.sys";


        public static void UpdateRootPassHash()
        {
            rootpass_md5 = AIC_Framework.Crypto.MD5.hash(rpf);
        }
        public static void UpdateUserPassHash()
        {
            usrpass_md5 = AIC_Framework.Crypto.MD5.hash(upf);
        }

        
        public static void WriteUserPass()
        {
            usrpass_md5 = AIC_Framework.Crypto.MD5.hash(usrpass);
            File.WriteAllText(upf, usrpass_md5);
        }
        public static void WriteRootPass()
        {
            usrpass_md5 = AIC_Framework.Crypto.MD5.hash(rootpass);
            File.WriteAllText(rpf, rootpass_md5);
        }
        public static string current_usr_dir = KernelVariables.homedir + KernelVariables.username;
    }
}
