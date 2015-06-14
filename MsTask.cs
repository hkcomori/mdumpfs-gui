using System;
using System.Runtime.InteropServices;

namespace Misuzilla.Tools.mdumpfs.Gui.Native
{
	public class Guids
	{
		private Guids() {}
		public static readonly Guid CTask = new Guid("148BD520-A2AB-11CE-B11F-00AA00530503");
		public static readonly Guid ITask = new Guid("148BD524-A2AB-11CE-B11F-00AA00530503");
	}

	public class TaskSchedulerFactory
	{
		private TaskSchedulerFactory() {}
		public static ITaskScheduler CreateTaskScheduler()
		{
			CTaskScheduler task = new CTaskScheduler();
			return task as ITaskScheduler;
		}
		public static ITask CreateTask()
		{
			CTask task = new CTask();
			return task as ITask;
		}
	}

	[ComVisible(false), ComImport]
	[Guid("148BD52A-A2AB-11CE-B11F-00AA00530503")]
	internal class CTaskScheduler
	{
	}

	[ComVisible(false), ComImport]
	[Guid("148BD520-A2AB-11CE-B11F-00AA00530503")]
	internal class CTask
	{
	}

	[Guid("148BD527-A2AB-11CE-B11F-00AA00530503")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ITaskScheduler
	{
		// Methods:
		void SetTargetComputer(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In] String pwszComputer);

		void GetTargetComputer(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszComputer);

		void Enum(
			[Out] out Object /*IEnumWorkItems*/ ppEnumWorkItems);

		void Activate(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String    pwszName,
			[In]  Guid      riid,
			[MarshalAs(UnmanagedType.IUnknown)]
			[Out] Object    ppUnk);

		void Delete(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In] String pwszName);

		void NewWorkItem(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszTaskName,
			[In]  Guid    rclsid,
			[In]  Guid    riid,
			[MarshalAs(UnmanagedType.IUnknown)]
			[Out] out Object ppUnk);

		void AddWorkItem(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In] String pwszTaskName,
			[MarshalAs(UnmanagedType.IUnknown)]
			[In] Object /* IScheduledWorkItem*/ pWorkItem);

		void IsOfType(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In] String pwszName,
			[In] Guid  riid);
	}


	[Guid("a6b952f0-a4b1-11d0-997d-00aa006887ec")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IScheduledWorkItem
	{
		// Methods concerning scheduling:
		void CreateTrigger(
			[Out] out Int16 piNewTrigger,
			[MarshalAs(UnmanagedType.IUnknown)]
			[Out] out Object /*ITaskTrigger ***/ ppTrigger);

		void DeleteTrigger(
			[In] Int16 iTrigger);

		void GetTriggerCount(
			[Out] out Int16 pwCount);

		void GetTrigger(
			[In]  Int16            iTrigger,
			[Out] out Object /*ITaskTrigger*/ ppTrigger);

		void GetTriggerString(
			[In]  Int16     iTrigger,
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszTrigger);

		void GetRunTimes(
			[In] /*LPSYSTEMTIME*/IntPtr pstBegin,
			[In] /*LPSYSTEMTIME*/IntPtr pstEnd,
			[In, Out] ref Int16  pCount,
			[Out] out /*LPSYSTEMTIME*/IntPtr rgstTaskTimes);

		void GetNextRunTime(
			[In, Out] ref /*LPSYSTEMTIME*/IntPtr pstNextRun);

		void SetIdleWait(
			[In]  Int16   wIdleMinutes,
			[In]  Int16   wDeadlineMinutes);
		void GetIdleWait(
			[Out] out Int16 pwIdleMinutes,
			[Out] out Int16 pwDeadlineMinutes);

		// Other methods:
		void Run();

		void Terminate();

		void EditWorkItem(
			[In] IntPtr hParent,
			[In] Int32 dwReserved);

		void GetMostRecentRunTime(
			[Out] out /*LPSYSTEMTIME*/IntPtr pstLastRun);

		void GetStatus(
			[Out] out IntPtr phrStatus);

		void GetExitCode(
			[Out] out Int32 pdwExitCode);

		// Properties:
		void SetComment(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszComment);
		void GetComment(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszComment);

		void SetCreator(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszCreator);
		void GetCreator(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszCreator);

		void SetWorkItemData(
			[In] Int16 cbData,
			[In] IntPtr /*BYTE rgbData[]*/ rgbData);
		void GetWorkItemData(
			[Out] out Int16 pcbData,
			[Out] out IntPtr /*BYTE ** */ prgbData);

		void SetErrorRetryCount(
			[In]  Int16   wRetryCount);
		void GetErrorRetryCount(
			[Out] out Int16 pwRetryCount);

		void SetErrorRetryInterval(
			[In]  Int16   wRetryInterval);
		void GetErrorRetryInterval(
			[Out] out Int16 pwRetryInterval);

		void SetFlags(
			[In]  Int32   dwFlags);
		void GetFlags(
			[Out] out Int32 pdwFlags);

		void SetAccountInformation(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszAccountName,
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszPassword);
		void GetAccountInformation(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszAccountName);
	}


	[Guid("148BD524-A2AB-11CE-B11F-00AA00530503")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ITask /* : IScheduledWorkItem */
	{
		// Methods concerning scheduling:
		void CreateTrigger(
			[Out] out Int16 piNewTrigger,
			[MarshalAs(UnmanagedType.IUnknown)]
			[Out] out Object /*ITaskTrigger ***/ ppTrigger);

		void DeleteTrigger(
			[In] Int16 iTrigger);

		void GetTriggerCount(
			[Out] out Int16 pwCount);

		void GetTrigger(
			[In]  Int16            iTrigger,
			[Out] out Object /*ITaskTrigger*/ ppTrigger);

		void GetTriggerString(
			[In]  Int16     iTrigger,
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszTrigger);

		void GetRunTimes(
			[In] /*LPSYSTEMTIME*/IntPtr pstBegin,
			[In] /*LPSYSTEMTIME*/IntPtr pstEnd,
			[In, Out] ref Int16  pCount,
			[Out] out /*LPSYSTEMTIME*/IntPtr rgstTaskTimes);

		void GetNextRunTime(
			[In, Out] ref /*LPSYSTEMTIME*/IntPtr pstNextRun);

		void SetIdleWait(
			[In]  Int16   wIdleMinutes,
			[In]  Int16   wDeadlineMinutes);
		void GetIdleWait(
			[Out] out Int16 pwIdleMinutes,
			[Out] out Int16 pwDeadlineMinutes);

		// Other methods:
		void Run();

		void Terminate();

		void EditWorkItem(
			[In] IntPtr hParent,
			[In] Int32 dwReserved);

		void GetMostRecentRunTime(
			[Out] out /*LPSYSTEMTIME*/IntPtr pstLastRun);

		void GetStatus(
			[Out] out IntPtr phrStatus);

		void GetExitCode(
			[Out] out Int32 pdwExitCode);

		// Properties:
		void SetComment(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszComment);
		void GetComment(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszComment);

		void SetCreator(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszCreator);
		void GetCreator(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszCreator);

		void SetWorkItemData(
			[In] Int16 cbData,
			[In] IntPtr /*BYTE rgbData[]*/ rgbData);
		void GetWorkItemData(
			[Out] out Int16 pcbData,
			[Out] out IntPtr /*BYTE ** */ prgbData);

		void SetErrorRetryCount(
			[In]  Int16   wRetryCount);
		void GetErrorRetryCount(
			[Out] out Int16 pwRetryCount);

		void SetErrorRetryInterval(
			[In]  Int16   wRetryInterval);
		void GetErrorRetryInterval(
			[Out] out Int16 pwRetryInterval);

		void SetFlags(
			[In]  Int32   dwFlags);
		void GetFlags(
			[Out] out Int32 pdwFlags);

		void SetAccountInformation(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszAccountName,
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszPassword);
		void GetAccountInformation(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszAccountName);

		// ITask
		void SetApplicationName(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszApplicationName);
		void GetApplicationName(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszApplicationName);

		void SetParameters(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszParameters);
		void GetParameters(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszParameters);

		void SetWorkingDirectory(
			[MarshalAs(UnmanagedType.LPWStr)]
			[In]  String  pwszWorkingDirectory);
		void GetWorkingDirectory(
			[MarshalAs(UnmanagedType.LPWStr)]
			[Out] out String ppwszWorkingDirectory);

		void SetPriority(
			[In]  Int32   dwPriority);
		void GetPriority(
			[Out] out Int32 pdwPriority);

		// Other properties:
		void SetTaskFlags(
			[In]  Int32   dwFlags);
		void GetTaskFlags(
			[Out] out Int32 pdwFlags);

		void SetMaxRunTime(
			[In]  Int32   dwMaxRunTimeMS);
		void GetMaxRunTime(
			[Out] out Int32 pdwMaxRunTimeMS);
	}
}
