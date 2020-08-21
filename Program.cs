using System;
using System.IO;
using System.Text;
using System.Text.Encodings;


namespace PkgSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            string exe_name = System.IO.Path.GetFileName(
                System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
                );
            string help = $"Using:\n {exe_name} PackageName.pkg \n\nResult:\n PackageName.spc\n PackageName.bdy";
            string suffix = "\n/\n\nexit";

            if (args.Length > 0)
            {
                Console.WriteLine($"Processing {args[0]}");
                string pkg_path = args[0];

                if (!File.Exists(pkg_path))
                {
                    Console.WriteLine("File does not exist or inaccessible...");
                    Console.WriteLine(help);
                }
                else
                {

                    string spc_path = $"{args[0].Split('.')[0]}.spc";
                    string bdy_path = $"{args[0].Split('.')[0]}.bdy";

                    Console.WriteLine($"Head: {spc_path}");
                    Console.WriteLine($"Body: {bdy_path}");

                    string spc_buff = String.Empty;
                    string bdy_buff = String.Empty;


                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    Encoding encoding = System.Text.Encoding.GetEncoding("windows-1251");

                    using (StreamReader pkg = new StreamReader(pkg_path, encoding))
                    {
                        string fl = pkg.ReadToEnd();
                        spc_buff = fl.Split("\n/\n")[0] + suffix;
                        bdy_buff = fl.Split("\n/\n")[1] + suffix;
                    }

                    using (FileStream spc_stream = new FileStream(spc_path, FileMode.Create))
                    {
                        using (StreamWriter spc = new StreamWriter(spc_stream, encoding))
                        {
                            spc.Write(spc_buff);
                            Console.WriteLine($"Head: {spc_buff.Length} symbols wrote into {spc_path}");
                        }

                    }

                    using (FileStream bdy_stream = new FileStream(bdy_path, FileMode.Create))
                    {
                        using (StreamWriter spc = new StreamWriter(bdy_stream, encoding))
                        {
                            spc.Write(bdy_buff);
                            Console.WriteLine($"Body: {bdy_buff.Length} symbols wrote into {bdy_path}");
                        }

                    }




                }
            }
            else
                Console.WriteLine(help);
        }
    }
}
