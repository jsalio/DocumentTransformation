using Boundaries.DocumentTransformation;
using Boundaries.DocumentTransformation.Utils;
using Boundaries.PdfEngine;
using IDRSNET;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrasnsformerSvc.Converter
{
    public sealed class IrisDocumentConverter : IPdfConverter
    {
        private readonly ServiceSettings setting;
        private CIDRSLicenseOcr m_objLicenseOcr;
        private CIDRSLicense m_objLicensePrepro;
        private CIDRSLicense m_objLicenseDocumentOutput;
        private CIDRSLicense m_objLicenseImageFile;

        private CIDRS _objIDRS;
        private CPageRecognition _objPageRecognition;
        private CDocumentOutputParameters _objOutputParam;

        public IrisDocumentConverter(EngineView ocrEngineView)
        {
            setting = new ServiceSettings();
            BuildAssets(ocrEngineView.Id);
        }

        private void BuildAssets(int engineId)
        {
            EngineLicenseView serialKeys = setting.GetEngineLicense(engineId);
            IEnumerable<IrisLicenses> licenses = JsonConvert.DeserializeObject<IEnumerable<IrisLicenses>>(serialKeys.LicenseString);
            BuildIrisObject(licenses);
        }

        private void BuildIrisObject(IEnumerable<IrisLicenses> licenses)
        {
            if (licenses.Count() == 0 || licenses.Count() < 6)
            {
                throw new Exception("Licence not match with required");
            }

            m_objLicenseOcr.ResourcePath = @"D:\Novosit\iDRS_15_6_8_LTS_243_Win64\bin";//licenses.FirstOrDefault(x => x.Key == LicenceType.OcrResourcePath).Value;
            m_objLicenseOcr = new CIDRSLicenseOcr(eLicenseType.Software, licenses.FirstOrDefault(x => x.Key == LicenceType.OcrModuleLicense).Value);
            m_objLicensePrepro = new CIDRSLicense(eModule.Prepro, eLicenseType.Software, licenses.FirstOrDefault(x => x.Key == LicenceType.OcrPreProcessingLicense).Value);
            m_objLicenseDocumentOutput = new CIDRSLicense(eModule.DocumentOutput, eLicenseType.Software, licenses.FirstOrDefault(x => x.Key == LicenceType.OcrDocumentOutputLicense).Value);
            m_objLicenseDocumentOutput.EnableExtension(Extension.DocumentOutputIHQC, licenses.FirstOrDefault(x => x.Key == LicenceType.OcrDocumentOutputIHQCLicense).Value);
            m_objLicenseImageFile = new CIDRSLicense(eModule.ImageFile, eLicenseType.Software, licenses.FirstOrDefault(x => x.Key == LicenceType.OcrImageFileLicense).Value);
            SetupModules();
        }

        private void Initialize()
        {
            _objIDRS = new CIDRS();
            _objPageRecognition = new CPageRecognition(_objIDRS);
            using (COcrContext objOcrContext = new COcrContext(new List<Language> { Language.Spanish, Language.English }))
            {
                _objPageRecognition.Context = objOcrContext;
            }

            using (CBinarizeOptions objBinarizeOptions = new CBinarizeOptions(true, false,
                true, 0))
            {
                _objPageRecognition.BinarizationOptions = objBinarizeOptions;
            }

            _objPageRecognition.DetectOrientation = false;
            var cPdfOutputParameters = new CPdfOutputParameters(
                CPdfOutputParameters.ePdfType.PDF_IMAGE_TEXT_IHQC, CPdfOutputParameters.ePdfVersion.PDF_1_4);
            CPdfIhqcParameters cPdfIhqcParameters = new CPdfIhqcParameters
            {
                CompressionLevel = CIhqcParameters.eCompressionLevel.LEVEL_III
            };
            cPdfOutputParameters.SetIhqcParameters(cPdfIhqcParameters);
            _objOutputParam = cPdfOutputParameters;
        }

        private bool SetupModules()
        {
            string strModule = "";
            try
            {
                CIDRSSetup.SetupModule(m_objLicenseOcr);
                CIDRSSetup.SetupModule(m_objLicensePrepro);
                CIDRSSetup.SetupModule(m_objLicenseDocumentOutput);
                CIDRSSetup.SetupModule(m_objLicenseImageFile);

                return true;
            }
            catch (CIDRSException argException)
            {
                #region Report exception
                Console.WriteLine("An error occured during iDRS setup.");
                Console.WriteLine("Source: " + argException.SrcFile);
                Console.WriteLine("Line: " + argException.SrcLine);
                Console.WriteLine("Code: " + (uint)argException.ErCode);
                string strError = "ERROR - Setting up " + strModule + " failed.";
                switch (argException.ErCode)
                {
                    case ErrorCode.TemporaryLicenseExpired:
                        strError += " Temporary license expired.";
                        break;
                    case ErrorCode.CharacterRecognitionEngineInvalidKey:
                        strError += " Please check your software keys for the OCR module.";
                        break;
                    case ErrorCode.BarcodeInvalidKey:
                        strError += " Please check your software keys for the BARCODE module.";
                        break;
                    case ErrorCode.PreproInvalidKey:
                        strError += " Please check your software keys for the PRE-PROCESSING module.";
                        break;
                    case ErrorCode.DocumentOutputInvalidKey:
                        strError += " Please check your software keys for the DOCUMENT_OUTPUT module.";
                        break;
                    case ErrorCode.ImageFileInvalidKey:
                        strError += " Please check your software keys for the IMAGE_FILE module.";
                        break;
                    case ErrorCode.SentinelNoServerRunning:
                        strError += "\nPossible reason: The Sentinel License Server is not running.";
                        break;
                    case ErrorCode.SentinelComputedIdMismatch:
                        strError += "\nPossible reason: The specified license is not valid on this machine, due to computer id mismatch.";
                        break;
                    case ErrorCode.SentinelTrialLicExhausted:
                        strError += "\nPossible reason: The duration of the specified trial license is exhausted.";
                        break;
                    case ErrorCode.SentinelInvalidLicense:
                        strError += "\nPossible reason: The given license code is invalid.";
                        break;
                    case ErrorCode.SentinelDuplicateLicense:
                        strError += "\nPossible reason: The given license code is already added to the specified license server.";
                        break;
                    case ErrorCode.SentinelComputerIdInvalid:
                        strError += "\nPossible reason: The specified computer id is invalid.";
                        break;
                    case ErrorCode.SentinelFailure:
                        strError += "\nPossible reason: An unknown idrs sentinel error occured.";
                        break;
                    case ErrorCode.SentinelIncorrectLicense:
                        strError += "\nPossible reason: The license is not correct.";
                        break;
                    case ErrorCode.SentinelLoadingLibrary:
                        strError += "\nPossible reason: The Sentinel library could not be loaded.";
                        break;
                    default:
                        strError += " Please check your settings and your license type.";
                        break;
                }
                Console.WriteLine(strError);
                Console.WriteLine("Press any key to continue ...");
                Console.Read();
                CIDRSSetup.Unload();
                return false;
                #endregion Report exception
            }
        }



        Task<string> IPdfConverter.GenerateDocument(IEnumerable<string> pages)
        {
            return Task<string>.Factory.StartNew(() => GenerateDocument(pages));
        }

        public void CreateDocumentMemory(IEnumerable<string> imagesPath, string outputFilePath)
        {
            Initialize();
            CStopwatch objStopwatchFormat = new CStopwatch();
            CStopwatch objStopwatchTotal = new CStopwatch();
            CStopwatch objStopwatchLoad = new CStopwatch();
            CStopwatch objStopwatchRead = new CStopwatch();

            CMemoryPageSet objPageSet = new CMemoryPageSet();

            try
            {
                foreach (string strInputFilePath in imagesPath)
                {
                    uint uiPageCount = CImage.GetPageCount(_objIDRS, strInputFilePath);
                    for (uint uiPageIndex = 0; uiPageIndex < uiPageCount; uiPageIndex++)
                    {
                        using (CPage objPage = ReadPage(_objIDRS, _objPageRecognition, objStopwatchTotal,
                            objStopwatchLoad,
                            objStopwatchRead, strInputFilePath, uiPageIndex))
                        {
                            objPageSet.Add(objPage);
                        }
                    }
                }

                objStopwatchTotal.Start(false);
                objStopwatchFormat.Start();
                using (CDocumentOutput objDocOutput = new CDocumentOutput(_objIDRS))
                {
                    objDocOutput.WorkerThreadCount = 2;
                    objDocOutput.Save(outputFilePath, _objOutputParam, objPageSet);
                }

                objStopwatchFormat.Stop();
                objStopwatchTotal.Stop();

                #region Show process durations

                uint uiHour = 0, uiMinute = 0, uiSecond = 0, uiMilliseconds = 0;
                objStopwatchTotal.GetElapsedTime(ref uiHour, ref uiMinute, ref uiSecond, ref uiMilliseconds);
                Console.WriteLine("Total elapsed time: " + uiMinute.ToString() + "m " + uiSecond.ToString() + "s " +
                                  uiMilliseconds.ToString() + "ms");

                #endregion //Show process durations
            }
            catch (CIDRSException argIdrsException)
            {
                if (argIdrsException.ErCode == ErrorCode.ImageFilePdfCorrupt)
                {
                    throw new System.Exception("Error in file Corrupted");
                }

                if (argIdrsException.ErCode == ErrorCode.MemoryAllocation)
                {
                    throw new System.Exception($"Error in memory. {argIdrsException.Message}");

                }

                Console.WriteLine(
                    $"error: {argIdrsException.Message}, inner: {argIdrsException.IdrsInnerException?.Message}");
                // Throw execption that might occur during page read
                throw new System.Exception($"Unmanage error occurs : {argIdrsException.Message}");
            }
            catch (System.AccessViolationException exception)
            {
                throw new System.Exception($"Unmanage error occurs : {exception.Message}");
            }
            finally
            {
                _objIDRS.Dispose();
                _objPageRecognition.Dispose();
                _objOutputParam.Dispose();
                objPageSet.Dispose();
                // dispose unused CStopWatch objects
                objStopwatchFormat.Dispose();
                objStopwatchTotal.Dispose();
                objStopwatchLoad.Dispose();
                objStopwatchRead.Dispose();
            }
        }

        string GenerateDocument(IEnumerable<string> pages)
        {
            string base64 = "";
            string tempDirectory = ConfigurationToProperty.GetKeyValue<string>("TempDirectory");
            string newFile = Path.Combine(tempDirectory, Guid.NewGuid().ToString() + ".pdf");

            Initialize();
            CStopwatch objStopwatchFormat = new CStopwatch();
            CStopwatch objStopwatchTotal = new CStopwatch();
            CStopwatch objStopwatchLoad = new CStopwatch();
            CStopwatch objStopwatchRead = new CStopwatch();

            CMemoryPageSet objPageSet = new CMemoryPageSet();

            try
            {
                foreach (string strInputFilePath in pages)
                {
                    uint uiPageCount = CImage.GetPageCount(_objIDRS, strInputFilePath);
                    for (uint uiPageIndex = 0; uiPageIndex < uiPageCount; uiPageIndex++)
                    {
                        using (CPage objPage = ReadPage(_objIDRS, _objPageRecognition, objStopwatchTotal,
                            objStopwatchLoad,
                            objStopwatchRead, strInputFilePath, uiPageIndex))
                        {
                            objPageSet.Add(objPage);
                        }
                    }
                }

                objStopwatchTotal.Start(false);
                objStopwatchFormat.Start();
                using (CDocumentOutput objDocOutput = new CDocumentOutput(_objIDRS))
                {
                    objDocOutput.WorkerThreadCount = 2;
                    objDocOutput.Save("outputFilePath", _objOutputParam, objPageSet);
                }

                objStopwatchFormat.Stop();
                objStopwatchTotal.Stop();

                #region Show process durations

                uint uiHour = 0, uiMinute = 0, uiSecond = 0, uiMilliseconds = 0;
                objStopwatchTotal.GetElapsedTime(ref uiHour, ref uiMinute, ref uiSecond, ref uiMilliseconds);
                Console.WriteLine("Total elapsed time: " + uiMinute.ToString() + "m " + uiSecond.ToString() + "s " +
                                  uiMilliseconds.ToString() + "ms");

                #endregion //Show process durations
            }
            catch (CIDRSException argIdrsException)
            {
                if (argIdrsException.ErCode == ErrorCode.ImageFilePdfCorrupt)
                {
                    throw new System.Exception("Error in file Corrupted");
                }

                if (argIdrsException.ErCode == ErrorCode.MemoryAllocation)
                {
                    throw new System.Exception($"Error in memory. {argIdrsException.Message}");

                }

                Console.WriteLine(
                    $"error: {argIdrsException.Message}, inner: {argIdrsException.IdrsInnerException?.Message}");
                // Throw execption that might occur during page read
                throw new System.Exception($"Unmanage error occurs : {argIdrsException.Message}");
            }
            catch (System.AccessViolationException exception)
            {
                throw new System.Exception($"Unmanage error occurs : {exception.Message}");
            }
            finally
            {
                _objIDRS.Dispose();
                _objPageRecognition.Dispose();
                _objOutputParam.Dispose();
                objPageSet.Dispose();
                // dispose unused CStopWatch objects
                objStopwatchFormat.Dispose();
                objStopwatchTotal.Dispose();
                objStopwatchLoad.Dispose();
                objStopwatchRead.Dispose();
            }

            return base64;
        }

        /// <summary>
        /// Read a page.
        /// </summary>
        /// <param name="argIdrs">The iDRS instance</param>
        /// <param name="argPageRecognition">The page recognition instance to use for recognizing the page</param>
        /// <param name="argStopwatchTotal">The stopwatch used to record total processing time</param>
        /// <param name="argStopwatchLoad">The stopwatch used to record image loading time</param>
        /// <param name="argStopwatchRead">The stopwatch used to record page reading time</param>
        /// <param name="strFilePath">The path of the input file to process</param>
        /// <param name="uiPageIndex">The index of the page to process</param>
        /// <returns>The page that has been read</returns>
        private CPage ReadPage(CIDRS argIdrs, CPageRecognition argPageRecognition,
            CStopwatch argStopwatchTotal, CStopwatch argStopwatchLoad, CStopwatch argStopwatchRead,
            string strFilePath, uint uiPageIndex)
        {
            CPage objPage = new CPage(argIdrs);
            argStopwatchTotal.Start(false);
            argStopwatchLoad.Start(false);
            // Load the page at provided index in the input file
            objPage.LoadSourceImage(strFilePath, true, uiPageIndex);

            using (CDeskew m_objDeskew = new CDeskew(argIdrs))
            {
                m_objDeskew.Deskew(objPage);
            }

            argStopwatchLoad.Stop();

            argStopwatchRead.Start(false);
            argPageRecognition.RecognizePage(objPage, RecognitionEngines.Ocr, true);
            argStopwatchRead.Stop();

            argStopwatchTotal.Stop();

            return objPage;
        }
    }
}
