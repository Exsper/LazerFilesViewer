using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Realms;
using osu.Game;


namespace LazerFilesViewer
{
    public static class Storage
    {
        public static string AddToStorage(string filePath, string storagePath)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] data = File.ReadAllBytes(filePath);
            byte[] hashBytes = sha256Hash.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            string hash = sb.ToString().ToLower();
            string storageFileFolder = storagePath + hash.Substring(0, 1) + "\\" + hash.Substring(0, 2) + "\\";

            if (!Directory.Exists(storageFileFolder))
            {
                Directory.CreateDirectory(storageFileFolder);
            }
            File.Copy(filePath, storageFileFolder + hash, true);

            return hash;
        }
    }
    public class Database
    {
        public static async Task<bool> AddtoRealm(Realm realm, RealmFile file)
        {
            if (!file.IsManaged)
            {
                /*
                using (var transaction = realm.BeginWrite())
                {
                    try
                    {
                        realm.Add(file);
                        if (transaction.State == TransactionState.Running)
                        {
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        if (transaction.State != TransactionState.RolledBack &&
                            transaction.State != TransactionState.Committed)
                        {
                            transaction.Rollback();
                        }
                    }
                }
                */
                await realm.WriteAsync(() =>
                {
                    realm.Add(file);
                });

            }
            return true;
        }

        public static async Task<bool> ChangeFile(Realm realm, RealmNamedFileUsage rnfu, RealmFile rf)
        {
            /*
            using (var transaction = realm.BeginWrite())
            {
                try
                {
                    rnfu.File = rf;
                    if (transaction.State == TransactionState.Running)
                    {
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (transaction.State != TransactionState.RolledBack &&
                        transaction.State != TransactionState.Committed)
                    {
                        transaction.Rollback();
                    }
                }
            }
            */
            await realm.WriteAsync(() =>
            {
                rnfu.File = rf;
            });

            return true;
        }
    } 
}
