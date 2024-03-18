using BackendProject.MiscUtils;
using System.Security.Cryptography;

namespace WebUtils.CDS
{
    public class BruteforceProcess
    {
        private byte[]? DecryptedFileBytes = null;
        private byte[]? EncryptedFileBytes = null;

        public BruteforceProcess(byte[] EncryptedFileBytes)
        {
            this.EncryptedFileBytes = EncryptedFileBytes;
        }

        public byte[]? StartBruteForce(int mode = 0)
        {
            if (EncryptedFileBytes != null)
            {
                DateTime timeStarted = DateTime.Now;
                CustomLogger.LoggerAccessor.LogWarn("[CDS] - BruteforceProcess - BruteForce started at: - {0}", timeStarted.ToString());

                byte[]? TempBuffer = VariousUtils.CopyBytes(EncryptedFileBytes, 0, 8);

                if (TempBuffer != null)
                {
                    DecryptedFileBytes = CTRExploitProcess.ProcessExploit(TempBuffer, EncryptedFileBytes, mode);

                    if (DecryptedFileBytes != null)
                        CustomLogger.LoggerAccessor.LogInfo("[CDS] - BruteforceProcess - Resolved SHA1: {0}", BitConverter.ToString(SHA1.Create().ComputeHash(DecryptedFileBytes)).Replace("-", string.Empty).ToUpper());
                    else
                        CustomLogger.LoggerAccessor.LogError("[CDS] - BruteforceProcess - Nothing matched! - Make sure input was correct. - {0}", DateTime.Now.ToString());
#if DEBUG
                    CustomLogger.LoggerAccessor.LogInfo("[CDS] - BruteforceProcess - Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
#endif
                }
                else
                    CustomLogger.LoggerAccessor.LogError("[CDS] - BruteforceProcess - The input data failed to copy! Make sure input was correct. - {0}", DateTime.Now.ToString());
            }

            return DecryptedFileBytes;
        }
    }
}