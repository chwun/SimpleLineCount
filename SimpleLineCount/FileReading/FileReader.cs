using System;
using System.Collections.Generic;
using System.Linq;
using SimpleLineCount.Counting;

namespace SimpleLineCount.FileReading
{
	public class FileReader : IFileReader
	{
		private readonly IFileAccess fileAccess;

		public FileReader(IFileAccess fileAccess)
		{
			this.fileAccess = fileAccess;
		}

		public InputFile ReadFile(string path)
		{
			try
			{
				string[] lines = fileAccess.ReadAllLines(path);

				return new InputFile(path, new(lines));
			}
			catch (Exception e)
			{
				return null; // TODO: return result object
			}
		}

		public List<InputFile> ReadFiles(string directory, LineCountParams lineCountParams)
		{
			List<InputFile> inputFiles = new();

			try
			{
				string[] files = fileAccess.GetAllDirectoryFiles(directory, lineCountParams.ScanFilesRecursively);

				foreach (string filePath in files)
				{
					string fileExtension = fileAccess.GetFileExtension(filePath);

					// ignore files if extension is not included
					if (!lineCountParams.FileOptions.IncludedFileExtensions.Any(x => x.Equals(fileExtension, StringComparison.OrdinalIgnoreCase)))
					{
						continue;
					}

					InputFile inputFile = ReadFile(filePath);

					if (inputFile == null)
					{
						continue; // TODO: fill result object
					}

					inputFiles.Add(inputFile);
				}
			}
			catch (Exception e)
			{
				inputFiles.Clear(); // TODO: return result object
			}

			return inputFiles;
		}
	}
}