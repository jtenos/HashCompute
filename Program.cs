using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

class Program
{
    private static void Main(string[] args)
    {
        if (args == null || !args.Any())
        {
            Console.WriteLine("You must select a file");
            Console.Read();
            Environment.Exit(1);
        }
        var fileInfo = new FileInfo(args[0]);
        if (!fileInfo.Exists)
        {
            Console.WriteLine("File not found");
            Console.Read();
            Environment.Exit(1);
        }
        try
        {
            Console.WriteLine("1: SHA1");
            Console.WriteLine("2: SHA256");
            Console.WriteLine("3: MD5");
            Console.WriteLine();
            Console.WriteLine("0: All");
            Console.WriteLine();
            Console.WriteLine("Your choice: ");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '0':
                {
                    using (SHA1 sha1 = SHA1.Create())
                    using (SHA256 sha256 = SHA256.Create())
                    using (MD5 md5 = MD5.Create())
                    {
                        Program.RunHash(sha1, fileInfo);
                        Program.RunHash(sha256, fileInfo);
                        Program.RunHash(md5, fileInfo);
                    }
                    break;
                }
                case '1':
                {
                    using (SHA1 sha1 = SHA1.Create())
                    {
                        Program.RunHash(sha1, fileInfo);
                    }
                    break;
                }
                case '2':
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        Program.RunHash(sha256, fileInfo);
                    }
                    break;
                }
                case '3':
                {
                    using (MD5 md5 = MD5.Create())
                    {
                        Program.RunHash(md5, fileInfo);
                    }
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid selection");
                    Console.Read();
                    Environment.Exit(1);
                    break;
                }
            }
            Console.WriteLine("DONE - Press <ENTER> to exit.");
            Console.Read();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Press <ENTER> to exit.");
            Console.Read();
            Environment.Exit(1);
        }
    }

    private static void RunHash(HashAlgorithm hashAlgorithm, FileInfo fi)
    {
        Console.WriteLine();
        Console.WriteLine("Hash: {0}", hashAlgorithm.GetType().FullName);
        using (FileStream fileStream = File.OpenRead(fi.FullName))
        {
            byte[] numArray = hashAlgorithm.ComputeHash(fileStream);
            Console.WriteLine("Base64: {0}", Convert.ToBase64String(numArray));
            Console.WriteLine("HEX:    {0}", string.Join("", (
                from b in (IEnumerable<byte>)numArray
                select b.ToString("X2")).ToArray<string>()));
            Console.WriteLine("hex:    {0}", string.Join("", (
                from b in (IEnumerable<byte>)numArray
                select b.ToString("x2")).ToArray<string>()));
        }
    }
}
