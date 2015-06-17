/*
 * mdumpfs.cs
 * $CVSId: mdumpfs.cs,v 1.6 2004/01/13 13:56:20 Mayuki Sawatari Exp $
 * $Id: mdumpfs.cs 4 2004-09-03 19:46:39Z tomoyo $
 *
 * Copyright (c) 2003 Mayuki Sawatari, All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE MISUZILLA.ORG ``AS IS'' AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
 * OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
 * OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
 * SUCH DAMAGE.
 */
 
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Misuzilla.Tools.mdumpfs
{
	public class ComandLineInterface
	{
		private static Verbosity _verbosity = Verbosity.DirectoryOnly;
		public static Int32
			Main(String[] args)
		{
			StartupInfo startup = new StartupInfo();
			startup.Error = new ErrorHandler(ErrorHandled);
			startup.DumpProgress = new DumpProgressHandler(DumpProgressHandled);
            
			// command line params
            Int32 i = 0;
            for (; (i < args.Length) && (args[i][0] == '-'); i++) 
			{
				switch (args[i].ToLower()) 
				{
						/*case "-e": case "/e":
							if (i < args.Length - 1) {
								reExclude = new Regex("("+Regex.Escape(args[i+1]).Replace(',', '|')+")");
								i++;
							}
							break;*/
					case "-re":
						if (i < args.Length - 1) 
						{
							startup.Exclude = new Regex(args[i+1]);
							i++;
						}
						break;
					case "-l":
						if (i < args.Length - 1) 
						{
							startup.PreviousDateLimit = Int32.Parse(args[i+1]);
							i++;
						}
						break;
					case "-v":
						if (i < args.Length - 1) 
						{
							try 
							{
								_verbosity = (Verbosity)Enum.Parse(typeof(Verbosity), args[i+1], true);
								i++;
							} 
							catch (System.ArgumentException) 
							{
								_verbosity = Verbosity.Detail;
							}
						} 
						else 
						{
							_verbosity = Verbosity.Detail;
						}
						break;
                    case "-d":
                        startup.DateMode = false;
                        break;
					case "-s": case "-q":
						_verbosity = Verbosity.Silent;
						break;
					case "-c":
						startup.CheckMode = true;
						break;
					case "-?": case "-h": case "--help":
						ShowUsage();
						return 0;
					default:
						Console.Error.WriteLine("未知のコマンドラインオプション \"{0}\" は無視されます。", args[i]);
						break;
				}
			}

            if (args.Length-i < 2)
            {
                ShowUsage();
                Console.Error.WriteLine("コマンドライン引数が不足しています。");
                return 1;
            }

			startup.SourceDirectory = Path.GetFullPath(args[args.Length-2] + Path.DirectorySeparatorChar);
			startup.DestinationRootDirectory = Path.GetFullPath(args[args.Length-1] + Path.DirectorySeparatorChar);

            if (!File.Exists(startup.SourceDirectory)) {
                Console.Error.WriteLine("ディレクトリが見つかりません: \"{0}\"", startup.SourceDirectory);
                return 1;
            }

			if (_verbosity == Verbosity.Debug) 
			{
				Console.WriteLine("srcDir:\t\t'{0}'\ndestRootDir:\t'{1}'\nbasename:\t'{2}'\nArg:\t\t'{3}'\n", startup.SourceDirectory, startup.DestinationRootDirectory, "", args[0]);
				Console.WriteLine("prevDir:\t'{0}'\ndestDir:\t'{1}'",
					Dumper.GetPreviousSnap(startup.DestinationRootDirectory, startup.StartDate, startup.PreviousDateLimit, startup.DateMode),
					startup.DestinationDirectory);
			}

			if (!FileUtility.IsNTFS(startup.DestinationRootDirectory)) 
			{
				Console.Error.WriteLine("エラー: バックアップ先のボリュームはNTFSでなければいけません。");
				return 1;
			} 
			else if (Environment.OSVersion.Version < new Version("5.0") ||
				Environment.OSVersion.Platform != PlatformID.Win32NT) 
			{
				Console.Error.WriteLine("エラー: このソフトウェアは Windows2000 以降の NTベースの Windows を必要とします。");
				return 1;
			}

			if (_verbosity == Verbosity.Silent)
				Console.SetOut(StreamWriter.Null);

			try 
			{
				Console.WriteLine("Destination Directory: {0}", startup.DestinationDirectory);
				Dumper.Copy(startup);
			} 
			catch (IOException ie) 
			{
				Console.Error.WriteLine("エラー: {0}", ie.Message);
				return 1;
			}

			return 0;
		}
		
		public static void
			ShowUsage()
		{
			Console.Error.WriteLine("ディレクトリのスナップショットを作成します。");
			Console.Error.WriteLine("");
            Console.Error.WriteLine("mdumpfs [-v [0-5]|-s] [-re regex] [-l num] コピー元ディレクトリ コピー先ディレクトリ\n");
			Console.Error.WriteLine("コマンドラインオプション:");
			Console.Error.WriteLine("  -v [0-5]\tコピー又はハードリンクしたファイルを表示する。");
			Console.Error.WriteLine("          \t0: 出力しない(-sと同じ)。");
			Console.Error.WriteLine("          \t1: 走査したディレクトリ名のみ表示。");
			Console.Error.WriteLine("          \t2: 1 に加えファイルを「.(ドット)」で表示する。");
			Console.Error.WriteLine("          \t3: ファイル名を表示する。");
			Console.Error.WriteLine("          \t4: 更新されたファイルのみ表示する。");
			Console.Error.WriteLine("          \t5: 全ての情報を表示する。");
            Console.Error.WriteLine("          \t6: デバッグ情報を表示する。");
            Console.Error.WriteLine("  -d\t\t日付で階層化しない。");
			Console.Error.WriteLine("  -s\t\t標準出力へ結果を出力をしない。");
			Console.Error.WriteLine("  -c\t\tチェックのみでバックアップを行わない。");
			Console.Error.WriteLine("  -re <regex>\t正規表現にマッチするディレクトリ/ファイルを対象外にする。");
			Console.Error.WriteLine("  -l <num>\t前回のスナップショットを探す日数(未指定時、31日)。");
			Console.Error.WriteLine("");
			Console.Error.WriteLine("前回(過去31日以内)のスナップショットと比較し更新されたファイルをコピーします。");
			Console.Error.WriteLine("更新されていないファイルは前回のスナップショットのファイルへのハードリンクとして保存されます。\n");
		}

		public static void
			ErrorHandled(String reason, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir)
		{
			Console.Error.WriteLine("エラー: {0}", reason);
		}
		public static void
			DumpProgressHandled(Int32 action, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir)
		{
			String srcFileName = Path.Combine(srcBaseDir, fileName);
			String destFileName = Path.Combine(destBaseDir, fileName);
			String prevFileName = Path.Combine(prevBaseDir, fileName);
			
			switch (action) 
			{
				case 0: // found
				switch (_verbosity) 
				{
					case Verbosity.UpdateOnly: break;
					case Verbosity.DirectoryAndFileCount:
						Console.Write("\n{0}", srcBaseDir); break;
					default:
						Console.WriteLine("{0}", srcBaseDir); break;
				}
					break;
				case 1: // hardlink
				switch (_verbosity) 
				{
					case Verbosity.Detail:
						Console.WriteLine("Hardlink: {0} -> {1}", prevFileName, destFileName); break;
					case Verbosity.FileName:
						Console.WriteLine("{0}", prevFileName); break;
					case Verbosity.DirectoryAndFileCount:
						Console.Write("."); break;
				}
					break;
				case 2: // copy
				switch (_verbosity) 
				{
					case Verbosity.Detail:
						Console.WriteLine("Copy: {0} -> {1}", srcFileName, destFileName); break;
					case Verbosity.FileName:
					case Verbosity.UpdateOnly:
						Console.WriteLine("{0}", srcFileName); break;
					case Verbosity.DirectoryAndFileCount:
						Console.Write("."); break;
				}
					break;
			}
		}
	}
	
	public enum
		Verbosity
	{
		Silent = 0,
		DirectoryOnly,
		DirectoryAndFileCount,
		FileName,
		UpdateOnly,
		Detail,
		Debug,
	}
	
	public delegate void DumpProgressHandler(Int32 action, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir);
	public delegate void ErrorHandler(String reason, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir);
	public class Dumper
	{
		private StartupInfo _startInfo;
		
		public Dumper(StartupInfo start)
		{
			_startInfo = start;
		}
		
		public static Boolean
			Copy(StartupInfo start)
		{
			return (new Dumper(start)).Copy();
		}
		public Boolean
			Copy()
		{
			return CopyInternal(_startInfo.SourceDirectory, _startInfo.DestinationDirectory, GetPreviousSnap());
		}
		
		private Boolean
			CopyInternal(String srcBaseDir, String destBaseDir, String prevBaseDir)
		{
			FileInfo[] files;
			OnDumpProgress(0, "", srcBaseDir, destBaseDir, prevBaseDir);

			try 
			{
				if (!_startInfo.CheckMode) Directory.CreateDirectory(destBaseDir);
				files = new DirectoryInfo(srcBaseDir).GetFiles();
			} 
			catch (SystemException e) 
			{
				OnError(e.Message, "", srcBaseDir, destBaseDir, prevBaseDir);
				return false;
			}
			
			// file
			foreach (FileInfo f in files) 
			{
				// exclude
				if (_startInfo.Exclude != null && _startInfo.Exclude.IsMatch(f.Name)) continue;
				
				String srcFileName = Path.Combine(srcBaseDir, f.Name);
				String destFileName = Path.Combine(destBaseDir, f.Name);

				try 
				{
					String prevFileName = Path.Combine(prevBaseDir, f.Name);
					if (prevBaseDir != "" && 
						FileUtility.IsSameFile(prevFileName, srcFileName)) 
					{
						// hard link
						if (!_startInfo.CheckMode) FileUtility.CreateHardLink(prevFileName, destFileName);
						OnDumpProgress(1, f.Name, srcBaseDir, destBaseDir, prevBaseDir);
					} 
					else 
					{
						// copy
						if (!_startInfo.CheckMode) File.Copy(srcFileName, destFileName, true);
						OnDumpProgress(2, f.Name, srcBaseDir, destBaseDir, prevBaseDir);
					}
				} 
				catch (UnauthorizedAccessException uae) 
				{
					OnError(uae.Message, f.Name, srcBaseDir, destBaseDir, prevBaseDir);
					if (!_startInfo.IgnoreError) return false;
				} 
				catch (IOException ie) 
				{
					OnError(ie.Message, f.Name, srcBaseDir, destBaseDir, prevBaseDir);
					if (!_startInfo.IgnoreError) return false;
				}
			}
			
			// directory
			foreach (String dir in Directory.GetDirectories(srcBaseDir)) 
			{
				String dirName = Path.GetFileName(dir);
				if (_startInfo.Exclude != null && _startInfo.Exclude.IsMatch(dir+Path.DirectorySeparatorChar)) continue;
				
				if (!CopyInternal(
					Path.Combine(srcBaseDir, dirName + Path.DirectorySeparatorChar), 
					Path.Combine(destBaseDir, dirName + Path.DirectorySeparatorChar), 
					(prevBaseDir != "") ? Path.Combine(prevBaseDir, dirName) : ""
					)) return false;
			}
			
			return true;
		}
		
		protected void
			OnDumpProgress(Int32 action, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir)
		{
			if (_startInfo.DumpProgress != null)
				_startInfo.DumpProgress(action, fileName, srcBaseDir, destBaseDir, prevBaseDir);
		}
		protected void
			OnError(String reason, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir)
		{
			if (_startInfo.Error != null)
				_startInfo.Error(reason, fileName, srcBaseDir, destBaseDir, prevBaseDir);
		}
		
		
		public String
			GetPreviousSnap()
		{
			String dirName = Path.GetFileName(_startInfo.SourceDirectory.TrimEnd(new Char[] {Path.DirectorySeparatorChar}));
			String prevDir = GetPreviousSnap(_startInfo.DestinationRootDirectory, _startInfo.StartDate, _startInfo.PreviousDateLimit, _startInfo.DateMode);
			if (prevDir != "") prevDir = Path.Combine(prevDir, dirName);
			return prevDir;
		}
		
		public static String
			GetPreviousSnap(String destRootDir, DateTime baseDate, Int32 prevLimit, Boolean dateMode)
		{
			// -1日とか
			if (prevLimit < 1) throw new ArgumentException("前回実行日の制限が不正です");

			for (Int32 i = 0; i < prevLimit; i++) 
			{
				DateTime prevDate = baseDate.AddDays(~i);
                String prevDir;
                if (dateMode) {
                    prevDir = Path.Combine(destRootDir, prevDate.ToString("yyyy") + Path.DirectorySeparatorChar + prevDate.ToString("MM") + Path.DirectorySeparatorChar + prevDate.ToString("dd"));
                }
                else
                {
                    prevDir = Path.Combine(destRootDir, prevDate.ToString("yyyyMMdd"));
                }
				if (Directory.Exists(prevDir)) return prevDir;
			}
			return "";
		}
	}
	
	public class StartupInfo
	{
		private String _destDir;
		private String _srcDir;
        private Boolean _dateMode = true;
		private DateTime _startDate = DateTime.Now;
		private Int32 _prevDateLimit = 31;
		private Regex _reExclude = null;
		private Int64 _excludeSize = 0;
		private Boolean _ignoreError = false;
		private Boolean _checkMode = false;
		
		public ErrorHandler Error;
		public DumpProgressHandler DumpProgress;
		
		public StartupInfo()
		{
		}
		public StartupInfo(String srcBaseDir, String destBaseDir)
		{
			_srcDir = srcBaseDir;
			_destDir = destBaseDir;
		}
		
        public Boolean
            DateMode
        {
            get { return _dateMode; }
            set { _dateMode = value; }
        }
		public Boolean
			CheckMode
		{
			get { return _checkMode; }
			set { _checkMode = value; }
		}
		public Boolean
			IgnoreError
		{
			get { return _ignoreError; }
			set { _ignoreError = value; }
		}
		public Regex
			Exclude
		{
			get { return _reExclude; }
			set { _reExclude = value; }
		}
		public Int64
			ExcludeSize
		{
			get { return _excludeSize; }
			set { _excludeSize = value; }
		}
		public Int32
			PreviousDateLimit
		{
			get { return _prevDateLimit; }
			set { _prevDateLimit = value; }
		}
		public DateTime
			StartDate
		{
			get { return _startDate; }
			set { _startDate = value; }
		}
		public String
			DestinationRootDirectory
		{
			get { return _destDir; }
			set { _destDir = value; }
		}
		public String
			DestinationDirectory
		{
			get
			{
				String dirName = Path.GetFileName(_srcDir.TrimEnd(new Char[] {Path.DirectorySeparatorChar}));
                String dateName;
                if (_dateMode)
                {
                    dateName = 
                        DateTime.Now.ToString("yyyy") + Path.DirectorySeparatorChar +
                        DateTime.Now.ToString("MM") + Path.DirectorySeparatorChar +
                        DateTime.Now.ToString("dd");
                }
                else
                {
                    dateName = DateTime.Now.ToString("yyyyMMdd");
                }
				return Path.Combine(
					Path.Combine(
					_destDir,
                    dateName
					), dirName) + Path.DirectorySeparatorChar;
			}
		}
		public String
			SourceDirectory
		{
			get { return _srcDir; }
			set { _srcDir = value; }
		}
	}
	
	
	internal class FileUtility
	{
		public static Boolean
			IsSameFile(String file1, String file2)
		{
			if (!File.Exists(file1) || !File.Exists(file2)) return false;
			
			FileInfo fi1 = new FileInfo(file1);
			FileInfo fi2 = new FileInfo(file2);
			return fi1.Length == fi2.Length && fi1.LastWriteTime == fi2.LastWriteTime;
		}
		
		public static Boolean
			IsNTFS(String rootPath)
		{
			StringBuilder sb = new StringBuilder(255);
			GetVolumeInformation(Directory.GetDirectoryRoot(rootPath), IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, sb, 255);

			return sb.ToString().CompareTo("NTFS") == 0;
		}
		public static void
			CreateHardLink(String existingFileName, String newFileName)
		{
			//if (!CreateHardLinkAPI(newFileName, existingFileName, IntPtr.Zero)) {
			//	throw new IOException("CreateHardLink API が失敗しました。");
			//}
			CreateHardLinkAPI(newFileName, existingFileName, IntPtr.Zero);
		}
		
		[DllImport("Kernel32.Dll", CharSet=CharSet.Unicode)]
		private static extern Boolean
			GetVolumeInformation(
			String lpRootPathName,
			IntPtr /*StringBulider*/ lpVolumeNameBuffer,
			Int32 nVolumeNameSize,
			/*ref*/ IntPtr lpVolumeSerialNumber,
			/*ref*/ IntPtr lpMaximumComponentLength,
			/*ref*/ IntPtr lpFileSystemFlags,
			StringBuilder lpFileSystemNameBuffer,
			Int32 nFileSystemNameSize
			);
		
		[DllImport("Kernel32.Dll", CharSet=CharSet.Unicode, EntryPoint="CreateHardLink")]
		private static extern Boolean
			CreateHardLinkAPI(
			[MarshalAs(UnmanagedType.LPTStr)] String lpFileName,
			[MarshalAs(UnmanagedType.LPTStr)] String lpExistingFileName,
			IntPtr /* LPSECURITY_ATTRIBUTES */ lpSecurityAttributes
			);
	}
}