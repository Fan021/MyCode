' BasicInfo "http://www.activevb.de/cgi-bin/tippupload/show/329/Netzlaufwerk_verbinden_und_trennen_mit_VB_net"

'Author		Frank Dümpelmann
'Version	1.0.0.0 
'Build		2011_05_27_00
'

Public Class NetConnect

    ' Deklaration: Globale Form API-Konstanten
    Protected Const RESOURCETYPE_DISK As Integer = &H1

    ' Deklaration: Globale Form API-Typen
    Protected Structure NETRESOURCE
        Public dwScope As Integer
        Public dwType As Integer
        Public dwDisplayType As Integer
        Public dwUsage As Integer
        Public lpLocalName As String
        Public lpRemoteName As String
        Public lpComment As String
        Public lpProvider As String
    End Structure

    ' Deklaration: Globale Form API-Funktionen
    Protected Declare Function WNetAddConnection2 Lib "mpr.dll" _
     Alias "WNetAddConnection2A" ( _
     ByRef lpNetResource As NETRESOURCE, _
     ByVal lpPassword As String, _
     ByVal lpUserName As String, _
     ByVal dwFlags As Integer) As Integer

    Protected Declare Function WNetCancelConnection2 Lib "mpr.dll" _
     Alias "WNetCancelConnection2A" ( _
     ByVal lpName As String, _
     ByVal dwFlags As Integer, _
     ByVal fForce As Integer) As Integer

    Protected _UserName As String = ""
    Protected _UserPassword As String = ""
    Dim _NetzResource As NETRESOURCE

    Protected Enum ErrorCodes

        ' -----------------------------------------
        ' Win32 API error code definitions
        ' -----------------------------------------
        ' This section contains the error code definitions for the Win32 API functions.

        ' NO_ERROR
        NO_ERROR = 0 '  dderror

        ' The configuration registry database operation completed successfully.
        ERROR_SUCCESS = 0&

        '   Incorrect function.
        ERROR_INVALID_FUNCTION = 1 '  dderror

        '   The system cannot find the file specified.
        ERROR_FILE_NOT_FOUND = 2&

        '   The system cannot find the path specified.
        ERROR_PATH_NOT_FOUND = 3&

        '   The system cannot open the file.
        ERROR_TOO_MANY_OPEN_FILES = 4&

        '   Access is denied.
        ERROR_ACCESS_DENIED = 5&

        '   The handle is invalid.
        ERROR_INVALID_HANDLE = 6&

        '   The storage control blocks were destroyed.
        ERROR_ARENA_TRASHED = 7&

        '   Not enough storage is available to process this command.
        ERROR_NOT_ENOUGH_MEMORY = 8 '  dderror

        '   The storage control block address is invalid.
        ERROR_INVALID_BLOCK = 9&

        '   The environment is incorrect.
        ERROR_BAD_ENVIRONMENT = 10&

        '   An attempt was made to load a program with an
        '   incorrect format.
        ERROR_BAD_FORMAT = 11&

        '   The access code is invalid.
        ERROR_INVALID_ACCESS = 12&

        '   The data is invalid.
        ERROR_INVALID_DATA = 13&

        '   Not enough storage is available to complete this operation.
        ERROR_OUTOFMEMORY = 14&

        '   The system cannot find the drive specified.
        ERROR_INVALID_DRIVE = 15&

        '   The directory cannot be removed.
        ERROR_CURRENT_DIRECTORY = 16&

        '   The system cannot move the file
        '   to a different disk drive.
        ERROR_NOT_SAME_DEVICE = 17&

        '   There are no more files.
        ERROR_NO_MORE_FILES = 18&

        '   The media is write protected.
        ERROR_WRITE_PROTECT = 19&

        '   The system cannot find the device specified.
        ERROR_BAD_UNIT = 20&

        '   The device is not ready.
        ERROR_NOT_READY = 21&

        '   The device does not recognize the command.
        ERROR_BAD_COMMAND = 22&

        '   Data error (cyclic redundancy check)
        ERROR_CRC = 23&

        '   The program issued a command but the
        '   command length is incorrect.
        ERROR_BAD_LENGTH = 24&

        '   The drive cannot locate a specific
        '   area or track on the disk.
        ERROR_SEEK = 25&

        '   The specified disk or diskette cannot be accessed.
        ERROR_NOT_DOS_DISK = 26&

        '   The drive cannot find the sector requested.
        ERROR_SECTOR_NOT_FOUND = 27&

        '   The printer is out of paper.
        ERROR_OUT_OF_PAPER = 28&

        '   The system cannot write to the specified device.
        ERROR_WRITE_FAULT = 29&

        '   The system cannot read from the specified device.
        ERROR_READ_FAULT = 30&

        '   A device attached to the system is not functioning.
        ERROR_GEN_FAILURE = 31&

        '   The process cannot access the file because
        '   it is being used by another process.
        ERROR_SHARING_VIOLATION = 32&

        '   The process cannot access the file because
        '   another process has locked a portion of the file.
        ERROR_LOCK_VIOLATION = 33&

        '   The wrong diskette is in the drive.
        '   Insert %2 (Volume Serial Number: %3)
        '   into drive %1.
        ERROR_WRONG_DISK = 34&

        '   Too many files opened for sharing.
        ERROR_SHARING_BUFFER_EXCEEDED = 36&

        '   Reached end of file.
        ERROR_HANDLE_EOF = 38&

        '   The disk is full.
        ERROR_HANDLE_DISK_FULL = 39&

        '   The network request is not supported.
        ERROR_NOT_SUPPORTED = 50&

        '   The remote computer is not available.
        ERROR_REM_NOT_LIST = 51&

        '   A duplicate name exists on the network.
        ERROR_DUP_NAME = 52&

        '   The network path was not found.
        ERROR_BAD_NETPATH = 53&

        '   The network is busy.
        ERROR_NETWORK_BUSY = 54&

        '   The specified network resource or device is no longer
        '   available.
        ERROR_DEV_NOT_EXIST = 55 '  dderror

        '   The network BIOS command limit has been reached.
        ERROR_TOO_MANY_CMDS = 56&

        '   A network adapter hardware error occurred.
        ERROR_ADAP_HDW_ERR = 57&

        '   The specified server cannot perform the requested
        '   operation.
        ERROR_BAD_NET_RESP = 58&

        '   An unexpected network error occurred.
        ERROR_UNEXP_NET_ERR = 59&

        '   The remote adapter is not compatible.
        ERROR_BAD_REM_ADAP = 60&

        '   The printer queue is full.
        ERROR_PRINTQ_FULL = 61&

        '   Space to store the file waiting to be printed is
        '   not available on the server.
        ERROR_NO_SPOOL_SPACE = 62&

        '   Your file waiting to be printed was deleted.
        ERROR_PRINT_CANCELLED = 63&

        '   The specified network name is no longer available.
        ERROR_NETNAME_DELETED = 64&

        '   Network access is denied.
        ERROR_NETWORK_ACCESS_DENIED = 65&

        '   The network resource type is not correct.
        ERROR_BAD_DEV_TYPE = 66&

        '   The network name cannot be found.
        ERROR_BAD_NET_NAME = 67&

        '   The name limit for the local computer network
        '   adapter card was exceeded.
        ERROR_TOO_MANY_NAMES = 68&

        '   The network BIOS session limit was exceeded.
        ERROR_TOO_MANY_SESS = 69&

        '   The remote server has been paused or is in the
        '   process of being started.
        ERROR_SHARING_PAUSED = 70&

        '   The network request was not accepted.
        ERROR_REQ_NOT_ACCEP = 71&

        '   The specified printer or disk device has been paused.
        ERROR_REDIR_PAUSED = 72&

        '   The file exists.
        ERROR_FILE_EXISTS = 80&

        '   The directory or file cannot be created.
        ERROR_CANNOT_MAKE = 82&

        '   Fail on INT 24
        ERROR_FAIL_I24 = 83&

        '   Storage to process this request is not available.
        ERROR_OUT_OF_STRUCTURES = 84&

        '   The local device name is already in use.
        ERROR_ALREADY_ASSIGNED = 85&

        '   The specified network password is not correct.
        ERROR_INVALID_PASSWORD = 86&

        '   The parameter is incorrect.
        ERROR_INVALID_PARAMETER = 87 '  dderror

        '   A write fault occurred on the network.
        ERROR_NET_WRITE_FAULT = 88&

        '   The system cannot start another process at
        '   this time.
        ERROR_NO_PROC_SLOTS = 89&

        '   Cannot create another system semaphore.
        ERROR_TOO_MANY_SEMAPHORES = 100&

        '   The exclusive semaphore is owned by another process.
        ERROR_EXCL_SEM_ALREADY_OWNED = 101&

        '   The semaphore is set and cannot be closed.
        ERROR_SEM_IS_SET = 102&

        '   The semaphore cannot be set again.
        ERROR_TOO_MANY_SEM_REQUESTS = 103&

        '   Cannot request exclusive semaphores at interrupt time.
        ERROR_INVALID_AT_INTERRUPT_TIME = 104&

        '   The previous ownership of this semaphore has ended.
        ERROR_SEM_OWNER_DIED = 105&

        '   Insert the diskette for drive %1.
        ERROR_SEM_USER_LIMIT = 106&

        '   Program stopped because alternate diskette was not inserted.
        ERROR_DISK_CHANGE = 107&

        '   The disk is in use or locked by
        '   another process.
        ERROR_DRIVE_LOCKED = 108&

        '   The pipe has been ended.
        ERROR_BROKEN_PIPE = 109&

        '   The system cannot open the
        '   device or file specified.
        ERROR_OPEN_FAILED = 110&

        '   The file name is too long.
        ERROR_BUFFER_OVERFLOW = 111&

        '   There is not enough space on the disk.
        ERROR_DISK_FULL = 112&

        '   No more internal file identifiers available.
        ERROR_NO_MORE_SEARCH_HANDLES = 113&

        '   The target internal file identifier is incorrect.
        ERROR_INVALID_TARGET_HANDLE = 114&

        '   The IOCTL call made by the application program is
        '   not correct.
        ERROR_INVALID_CATEGORY = 117&

        '   The verify-on-write switch parameter value is not
        '   correct.
        ERROR_INVALID_VERIFY_SWITCH = 118&

        '   The system does not support the command requested.
        ERROR_BAD_DRIVER_LEVEL = 119&

        '   This function is only valid in Windows NT mode.
        ERROR_CALL_NOT_IMPLEMENTED = 120&

        '   The semaphore timeout period has expired.
        ERROR_SEM_TIMEOUT = 121&

        '   The data area passed to a system call is too
        '   small.
        ERROR_INSUFFICIENT_BUFFER = 122 '  dderror

        '   The filename, directory name, or volume label syntax is incorrect.
        ERROR_INVALID_NAME = 123&

        '   The system call level is not correct.
        ERROR_INVALID_LEVEL = 124&

        '   The disk has no volume label.
        ERROR_NO_VOLUME_LABEL = 125&

        '   The specified module could not be found.
        ERROR_MOD_NOT_FOUND = 126&

        '   The specified procedure could not be found.
        ERROR_PROC_NOT_FOUND = 127&

        '   There are no child processes to wait for.
        ERROR_WAIT_NO_CHILDREN = 128&

        '   The %1 application cannot be run in Windows NT mode.
        ERROR_CHILD_NOT_COMPLETE = 129&

        '   Attempt to use a file handle to an open disk partition for an
        '   operation other than raw disk I/O.
        ERROR_DIRECT_ACCESS_HANDLE = 130&

        '   An attempt was made to move the file pointer before the beginning of the file.
        ERROR_NEGATIVE_SEEK = 131&

        '   The file pointer cannot be set on the specified device or file.
        ERROR_SEEK_ON_DEVICE = 132&

        '   A JOIN or SUBST command
        '   cannot be used for a drive that
        '   contains previously joined drives.
        ERROR_IS_JOIN_TARGET = 133&

        '   An attempt was made to use a
        '   JOIN or SUBST command on a drive that has
        '   already been joined.
        ERROR_IS_JOINED = 134&

        '   An attempt was made to use a
        '   JOIN or SUBST command on a drive that has
        '   already been substituted.
        ERROR_IS_SUBSTED = 135&

        '   The system tried to delete
        '   the JOIN of a drive that is not joined.
        ERROR_NOT_JOINED = 136&

        '   The system tried to delete the
        '   substitution of a drive that is not substituted.
        ERROR_NOT_SUBSTED = 137&

        '   The system tried to join a drive
        '   to a directory on a joined drive.
        ERROR_JOIN_TO_JOIN = 138&

        '   The system tried to substitute a
        '   drive to a directory on a substituted drive.
        ERROR_SUBST_TO_SUBST = 139&

        '   The system tried to join a drive to
        '   a directory on a substituted drive.
        ERROR_JOIN_TO_SUBST = 140&

        '   The system tried to SUBST a drive
        '   to a directory on a joined drive.
        ERROR_SUBST_TO_JOIN = 141&

        '   The system cannot perform a JOIN or SUBST at this time.
        ERROR_BUSY_DRIVE = 142&

        '   The system cannot join or substitute a
        '   drive to or for a directory on the same drive.
        ERROR_SAME_DRIVE = 143&

        '   The directory is not a subdirectory of the root directory.
        ERROR_DIR_NOT_ROOT = 144&

        '   The directory is not empty.
        ERROR_DIR_NOT_EMPTY = 145&

        '   The path specified is being used in
        '   a substitute.
        ERROR_IS_SUBST_PATH = 146&

        '   Not enough resources are available to
        '   process this command.
        ERROR_IS_JOIN_PATH = 147&

        '   The path specified cannot be used at this time.
        ERROR_PATH_BUSY = 148&

        '   An attempt was made to join
        '   or substitute a drive for which a directory
        '   on the drive is the target of a previous
        '   substitute.
        ERROR_IS_SUBST_TARGET = 149&

        '   System trace information was not specified in your
        '   CONFIG.SYS file, or tracing is disallowed.
        ERROR_SYSTEM_TRACE = 150&

        '   The number of specified semaphore events for
        '   DosMuxSemWait is not correct.
        ERROR_INVALID_EVENT_COUNT = 151&

        '   DosMuxSemWait did not execute; too many semaphores
        '   are already set.
        ERROR_TOO_MANY_MUXWAITERS = 152&

        '   The DosMuxSemWait list is not correct.
        ERROR_INVALID_LIST_FORMAT = 153&

        '   The volume label you entered exceeds the
        '   11 character limit.  The first 11 characters were written
        '   to disk.  Any characters that exceeded the 11 character limit
        '   were automatically deleted.
        ERROR_LABEL_TOO_LONG = 154&

        '   Cannot create another thread.
        ERROR_TOO_MANY_TCBS = 155&

        '   The recipient process has refused the signal.
        ERROR_SIGNAL_REFUSED = 156&

        '   The segment is already discarded and cannot be locked.
        ERROR_DISCARDED = 157&

        '   The segment is already unlocked.
        ERROR_NOT_LOCKED = 158&

        '   The address for the thread ID is not correct.
        ERROR_BAD_THREADID_ADDR = 159&

        '   The argument string passed to DosExecPgm is not correct.
        ERROR_BAD_ARGUMENTS = 160&

        '   The specified path is invalid.
        ERROR_BAD_PATHNAME = 161&

        '   A signal is already pending.
        ERROR_SIGNAL_PENDING = 162&

        '   No more threads can be created in the system.
        ERROR_MAX_THRDS_REACHED = 164&

        '   Unable to lock a region of a file.
        ERROR_LOCK_FAILED = 167&

        '   The requested resource is in use.
        ERROR_BUSY = 170&

        '   A lock request was not outstanding for the supplied cancel region.
        ERROR_CANCEL_VIOLATION = 173&

        '   The file system does not support atomic changes to the lock type.
        ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 174&

        '   The system detected a segment number that was not correct.
        ERROR_INVALID_SEGMENT_NUMBER = 180&

        '   The operating system cannot run %1.
        ERROR_INVALID_ORDINAL = 182&

        '   Cannot create a file when that file already exists.
        ERROR_ALREADY_EXISTS = 183&

        '   The flag passed is not correct.
        ERROR_INVALID_FLAG_NUMBER = 186&

        '   The specified system semaphore name was not found.
        ERROR_SEM_NOT_FOUND = 187&

        '   The operating system cannot run %1.
        ERROR_INVALID_STARTING_CODESEG = 188&

        '   The operating system cannot run %1.
        ERROR_INVALID_STACKSEG = 189&

        '   The operating system cannot run %1.
        ERROR_INVALID_MODULETYPE = 190&

        '   Cannot run %1 in Windows NT mode.
        ERROR_INVALID_EXE_SIGNATURE = 191&

        '   The operating system cannot run %1.
        ERROR_EXE_MARKED_INVALID = 192&

        '   %1 is not a valid Windows NT application.
        ERROR_BAD_EXE_FORMAT = 193&

        '   The operating system cannot run %1.
        ERROR_ITERATED_DATA_EXCEEDS_64k = 194&

        '   The operating system cannot run %1.
        ERROR_INVALID_MINALLOCSIZE = 195&

        '   The operating system cannot run this
        '   application program.
        ERROR_DYNLINK_FROM_INVALID_RING = 196&

        '   The operating system is not presently
        '   configured to run this application.
        ERROR_IOPL_NOT_ENABLED = 197&

        '   The operating system cannot run %1.
        ERROR_INVALID_SEGDPL = 198&

        '   The operating system cannot run this
        '   application program.
        ERROR_AUTODATASEG_EXCEEDS_64k = 199&

        '   The code segment cannot be greater than or equal to 64KB.
        ERROR_RING2SEG_MUST_BE_MOVABLE = 200&

        '   The operating system cannot run %1.
        ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 201&

        '   The operating system cannot run %1.
        ERROR_INFLOOP_IN_RELOC_CHAIN = 202&

        '   The system could not find the environment
        '   option that was entered.
        ERROR_ENVVAR_NOT_FOUND = 203&

        '   No process in the command subtree has a
        '   signal handler.
        ERROR_NO_SIGNAL_SENT = 205&

        '   The filename or extension is too long.
        ERROR_FILENAME_EXCED_RANGE = 206&

        '   The ring 2 stack is in use.
        ERROR_RING2_STACK_IN_USE = 207&

        '   The  filename characters,  or ?, are entered
        '   incorrectly or too many  filename characters are specified.
        ERROR_META_EXPANSION_TOO_LONG = 208&

        '   The signal being posted is not correct.
        ERROR_INVALID_SIGNAL_NUMBER = 209&

        '   The signal handler cannot be set.
        ERROR_THREAD_1_INACTIVE = 210&

        '   The segment is locked and cannot be reallocated.
        ERROR_LOCKED = 212&

        '   Too many dynamic link modules are attached to this
        '   program or dynamic link module.
        ERROR_TOO_MANY_MODULES = 214&

        '   Can't nest calls to LoadModule.
        ERROR_NESTING_NOT_ALLOWED = 215&

        '   The pipe state is invalid.
        ERROR_BAD_PIPE = 230&

        '   All pipe instances are busy.
        ERROR_PIPE_BUSY = 231&

        '   The pipe is being closed.
        ERROR_NO_DATA = 232&

        '   No process is on the other end of the pipe.
        ERROR_PIPE_NOT_CONNECTED = 233&

        '   More data is available.
        ERROR_MORE_DATA = 234 '  dderror

        '   The session was cancelled.
        ERROR_VC_DISCONNECTED = 240&

        '   The specified extended attribute name was invalid.
        ERROR_INVALID_EA_NAME = 254&

        '   The extended attributes are inconsistent.
        ERROR_EA_LIST_INCONSISTENT = 255&

        '   No more data is available.
        ERROR_NO_MORE_ITEMS = 259&

        '   The Copy API cannot be used.
        ERROR_CANNOT_COPY = 266&

        '   The directory name is invalid.
        ERROR_DIRECTORY = 267&

        '   The extended attributes did not fit in the buffer.
        ERROR_EAS_DIDNT_FIT = 275&

        '   The extended attribute file on the mounted file system is corrupt.
        ERROR_EA_FILE_CORRUPT = 276&

        '   The extended attribute table file is full.
        ERROR_EA_TABLE_FULL = 277&

        '   The specified extended attribute handle is invalid.
        ERROR_INVALID_EA_HANDLE = 278&

        '   The mounted file system does not support extended attributes.
        ERROR_EAS_NOT_SUPPORTED = 282&

        '   Attempt to release mutex not owned by caller.
        ERROR_NOT_OWNER = 288&

        '   Too many posts were made to a semaphore.
        ERROR_TOO_MANY_POSTS = 298&

        '   The system cannot find message for message number 0x%1
        '   in message file for %2.
        ERROR_MR_MID_NOT_FOUND = 317&

        '   Attempt to access invalid address.
        ERROR_INVALID_ADDRESS = 487&

        '   Arithmetic result exceeded 32 bits.
        ERROR_ARITHMETIC_OVERFLOW = 534&

        '   There is a process on other end of the pipe.
        ERROR_PIPE_CONNECTED = 535&

        '   Waiting for a process to open the other end of the pipe.
        ERROR_PIPE_LISTENING = 536&

        '   Access to the extended attribute was denied.
        ERROR_EA_ACCESS_DENIED = 994&

        '   The I/O operation has been aborted because of either a thread exit
        '   or an application request.
        ERROR_OPERATION_ABORTED = 995&

        '   Overlapped I/O event is not in a signalled state.
        ERROR_IO_INCOMPLETE = 996&

        '   Overlapped I/O operation is in progress.
        ERROR_IO_PENDING = 997 '  dderror

        '   Invalid access to memory location.
        ERROR_NOACCESS = 998&

        '   Error performing inpage operation.
        ERROR_SWAPERROR = 999&

        '   Recursion too deep, stack overflowed.
        ERROR_STACK_OVERFLOW = 1001&

        '   The window cannot act on the sent message.
        ERROR_INVALID_MESSAGE = 1002&

        '   Cannot complete this function.
        ERROR_CAN_NOT_COMPLETE = 1003&

        '   Invalid flags.
        ERROR_INVALID_FLAGS = 1004&

        '   The volume does not contain a recognized file system.
        '   Please make sure that all required file system drivers are loaded and that the
        '   volume is not corrupt.
        ERROR_UNRECOGNIZED_VOLUME = 1005&

        '   The volume for a file has been externally altered such that the
        '   opened file is no longer valid.
        ERROR_FILE_INVALID = 1006&

        '   The requested operation cannot be performed in full-screen mode.
        ERROR_FULLSCREEN_MODE = 1007&

        '   An attempt was made to reference a token that does not exist.
        ERROR_NO_TOKEN = 1008&

        '   The configuration registry database is corrupt.
        ERROR_BADDB = 1009&

        '   The configuration registry key is invalid.
        ERROR_BADKEY = 1010&

        '   The configuration registry key could not be opened.
        ERROR_CANTOPEN = 1011&

        '   The configuration registry key could not be read.
        ERROR_CANTREAD = 1012&

        '   The configuration registry key could not be written.
        ERROR_CANTWRITE = 1013&

        '   One of the files in the Registry database had to be recovered
        '   by use of a log or alternate copy.  The recovery was successful.
        ERROR_REGISTRY_RECOVERED = 1014&

        '   The Registry is corrupt. The structure of one of the files that contains
        '   Registry data is corrupt, or the system's image of the file in memory
        '   is corrupt, or the file could not be recovered because the alternate
        '   copy or log was absent or corrupt.
        ERROR_REGISTRY_CORRUPT = 1015&

        '   An I/O operation initiated by the Registry failed unrecoverably.
        '   The Registry could not read in, or write out, or flush, one of the files
        '   that contain the system's image of the Registry.
        ERROR_REGISTRY_IO_FAILED = 1016&

        '   The system has attempted to load or restore a file into the Registry, but the
        '   specified file is not in a Registry file format.
        ERROR_NOT_REGISTRY_FILE = 1017&

        '   Illegal operation attempted on a Registry key which has been marked for deletion.
        ERROR_KEY_DELETED = 1018&

        '   System could not allocate the required space in a Registry log.
        ERROR_NO_LOG_SPACE = 1019&

        '   Cannot create a symbolic link in a Registry key that already
        '   has subkeys or values.
        ERROR_KEY_HAS_CHILDREN = 1020&

        '   Cannot create a stable subkey under a volatile parent key.
        ERROR_CHILD_MUST_BE_VOLATILE = 1021&

        '   A notify change request is being completed and the information
        '   is not being returned in the caller's buffer. The caller now
        '   needs to enumerate the files to find the changes.
        ERROR_NOTIFY_ENUM_DIR = 1022&

        '   A stop control has been sent to a service which other running services
        '   are dependent on.
        ERROR_DEPENDENT_SERVICES_RUNNING = 1051&

        '   The requested control is not valid for this service
        ERROR_INVALID_SERVICE_CONTROL = 1052&

        '   The service did not respond to the start or control request in a timely
        '   fashion.
        ERROR_SERVICE_REQUEST_TIMEOUT = 1053&

        '   A thread could not be created for the service.
        ERROR_SERVICE_NO_THREAD = 1054&

        '   The service database is locked.
        ERROR_SERVICE_DATABASE_LOCKED = 1055&

        '   An instance of the service is already running.
        ERROR_SERVICE_ALREADY_RUNNING = 1056&

        '   The account name is invalid or does not exist.
        ERROR_INVALID_SERVICE_ACCOUNT = 1057&

        '   The specified service is disabled and cannot be started.
        ERROR_SERVICE_DISABLED = 1058&

        '   Circular service dependency was specified.
        ERROR_CIRCULAR_DEPENDENCY = 1059&

        '   The specified service does not exist as an installed service.
        ERROR_SERVICE_DOES_NOT_EXIST = 1060&

        '   The service cannot accept control messages at this time.
        ERROR_SERVICE_CANNOT_ACCEPT_CTRL = 1061&

        '   The service has not been started.
        ERROR_SERVICE_NOT_ACTIVE = 1062&

        '   The service process could not connect to the service controller.
        ERROR_FAILED_SERVICE_CONTROLLER_CONNECT = 1063&

        '   An exception occurred in the service when handling the control request.
        ERROR_EXCEPTION_IN_SERVICE = 1064&

        '   The database specified does not exist.
        ERROR_DATABASE_DOES_NOT_EXIST = 1065&

        '   The service has returned a service-specific error code.
        ERROR_SERVICE_SPECIFIC_ERROR = 1066&

        '   The process terminated unexpectedly.
        ERROR_PROCESS_ABORTED = 1067&

        '   The dependency service or group failed to start.
        ERROR_SERVICE_DEPENDENCY_FAIL = 1068&

        '   The service did not start due to a logon failure.
        ERROR_SERVICE_LOGON_FAILED = 1069&

        '   After starting, the service hung in a start-pending state.
        ERROR_SERVICE_START_HANG = 1070&

        '   The specified service database lock is invalid.
        ERROR_INVALID_SERVICE_LOCK = 1071&

        '   The specified service has been marked for deletion.
        ERROR_SERVICE_MARKED_FOR_DELETE = 1072&

        '   The specified service already exists.
        ERROR_SERVICE_EXISTS = 1073&

        '   The system is currently running with the last-known-good configuration.
        ERROR_ALREADY_RUNNING_LKG = 1074&

        '   The dependency service does not exist or has been marked for
        '   deletion.
        ERROR_SERVICE_DEPENDENCY_DELETED = 1075&

        '   The current boot has already been accepted for use as the
        '   last-known-good control set.
        ERROR_BOOT_ALREADY_ACCEPTED = 1076&

        '   No attempts to start the service have been made since the last boot.
        ERROR_SERVICE_NEVER_STARTED = 1077&

        '   The name is already in use as either a service name or a service display
        '   name.
        ERROR_DUPLICATE_SERVICE_NAME = 1078&

        '   The physical end of the tape has been reached.
        ERROR_END_OF_MEDIA = 1100&

        '   A tape access reached a filemark.
        ERROR_FILEMARK_DETECTED = 1101&

        '   Beginning of tape or partition was encountered.
        ERROR_BEGINNING_OF_MEDIA = 1102&

        '   A tape access reached the end of a set of files.
        ERROR_SETMARK_DETECTED = 1103&

        '   No more data is on the tape.
        ERROR_NO_DATA_DETECTED = 1104&

        '   Tape could not be partitioned.
        ERROR_PARTITION_FAILURE = 1105&

        '   When accessing a new tape of a multivolume partition, the current
        '   blocksize is incorrect.
        ERROR_INVALID_BLOCK_LENGTH = 1106&

        '   Tape partition information could not be found when loading a tape.
        ERROR_DEVICE_NOT_PARTITIONED = 1107&

        '   Unable to lock the media eject mechanism.
        ERROR_UNABLE_TO_LOCK_MEDIA = 1108&

        '   Unable to unload the media.
        ERROR_UNABLE_TO_UNLOAD_MEDIA = 1109&

        '   Media in drive may have changed.
        ERROR_MEDIA_CHANGED = 1110&

        '   The I/O bus was reset.
        ERROR_BUS_RESET = 1111&

        '   No media in drive.
        ERROR_NO_MEDIA_IN_DRIVE = 1112&

        '   No mapping for the Unicode character exists in the target multi-byte code page.
        ERROR_NO_UNICODE_TRANSLATION = 1113&

        '   A dynamic link library (DLL) initialization routine failed.
        ERROR_DLL_INIT_FAILED = 1114&

        '   A system shutdown is in progress.
        ERROR_SHUTDOWN_IN_PROGRESS = 1115&

        '   Unable to abort the system shutdown because no shutdown was in progress.
        ERROR_NO_SHUTDOWN_IN_PROGRESS = 1116&

        '   The request could not be performed because of an I/O device error.
        ERROR_IO_DEVICE = 1117&

        '   No serial device was successfully initialized.  The serial driver will unload.
        ERROR_SERIAL_NO_DEVICE = 1118&

        '   Unable to open a device that was sharing an interrupt request (IRQ)
        '   with other devices. At least one other device that uses that IRQ
        '   was already opened.
        ERROR_IRQ_BUSY = 1119&

        '   A serial I/O operation was completed by another write to the serial port.
        '   (The IOCTL_SERIAL_XOFF_COUNTER reached zero.)
        ERROR_MORE_WRITES = 1120&

        '   A serial I/O operation completed because the time-out period expired.
        '   (The IOCTL_SERIAL_XOFF_COUNTER did not reach zero.)
        ERROR_COUNTER_TIMEOUT = 1121&

        '   No ID address mark was found on the floppy disk.
        ERROR_FLOPPY_ID_MARK_NOT_FOUND = 1122&

        '   Mismatch between the floppy disk sector ID field and the floppy disk
        '   controller track address.
        ERROR_FLOPPY_WRONG_CYLINDER = 1123&

        '   The floppy disk controller reported an error that is not recognized
        '   by the floppy disk driver.
        ERROR_FLOPPY_UNKNOWN_ERROR = 1124&

        '   The floppy disk controller returned inconsistent results in its registers.
        ERROR_FLOPPY_BAD_REGISTERS = 1125&

        '   While accessing the hard disk, a recalibrate operation failed, even after retries.
        ERROR_DISK_RECALIBRATE_FAILED = 1126&

        '   While accessing the hard disk, a disk operation failed even after retries.
        ERROR_DISK_OPERATION_FAILED = 1127&

        '   While accessing the hard disk, a disk controller reset was needed, but
        '   even that failed.
        ERROR_DISK_RESET_FAILED = 1128&

        '   Physical end of tape encountered.
        ERROR_EOM_OVERFLOW = 1129&

        '   Not enough server storage is available to process this command.
        ERROR_NOT_ENOUGH_SERVER_MEMORY = 1130&

        '   A potential deadlock condition has been detected.
        ERROR_POSSIBLE_DEADLOCK = 1131&

        '   The base address or the file offset specified does not have the proper
        '   alignment.
        ERROR_MAPPED_ALIGNMENT = 1132&

        ' NEW for Win32
        ERROR_INVALID_PIXEL_FORMAT = 2000
        ERROR_BAD_DRIVER = 2001
        ERROR_INVALID_WINDOW_STYLE = 2002
        ERROR_METAFILE_NOT_SUPPORTED = 2003
        ERROR_TRANSFORM_NOT_SUPPORTED = 2004
        ERROR_CLIPPING_NOT_SUPPORTED = 2005
        ERROR_UNKNOWN_PRINT_MONITOR = 3000
        ERROR_PRINTER_DRIVER_IN_USE = 3001
        ERROR_SPOOL_FILE_NOT_FOUND = 3002
        ERROR_SPL_NO_STARTDOC = 3003
        ERROR_SPL_NO_ADDJOB = 3004
        ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED = 3005
        ERROR_PRINT_MONITOR_ALREADY_INSTALLED = 3006
        ERROR_WINS_INTERNAL = 4000
        ERROR_CAN_NOT_DEL_LOCAL_WINS = 4001
        ERROR_STATIC_INIT = 4002
        ERROR_INC_BACKUP = 4003
        ERROR_FULL_BACKUP = 4004
        ERROR_REC_NON_EXISTENT = 4005
        ERROR_RPL_NOT_ALLOWED = 4006
        SEVERITY_SUCCESS = 0
        SEVERITY_ERROR = 1
        FACILITY_NT_BIT = &H10000000
        NOERROR = 0
        E_UNEXPECTED = &H8000FFFF
        E_NOTIMPL = &H80004001
        E_OUTOFMEMORY = &H8007000E
        E_INVALIDARG = &H80070057
        E_NOINTERFACE = &H80004002
        E_POINTER = &H80004003
        E_HANDLE = &H80070006
        E_ABORT = &H80004004
        E_FAIL = &H80004005
        E_ACCESSDENIED = &H80070005
        CO_E_INIT_TLS = &H80004006
        CO_E_INIT_SHARED_ALLOCATOR = &H80004007
        CO_E_INIT_MEMORY_ALLOCATOR = &H80004008
        CO_E_INIT_CLASS_CACHE = &H80004009
        CO_E_INIT_RPC_CHANNEL = &H8000400A
        CO_E_INIT_TLS_SET_CHANNEL_CONTROL = &H8000400B
        CO_E_INIT_TLS_CHANNEL_CONTROL = &H8000400C
        CO_E_INIT_UNACCEPTED_USER_ALLOCATOR = &H8000400D
        CO_E_INIT_SCM_MUTEX_EXISTS = &H8000400E
        CO_E_INIT_SCM_FILE_MAPPING_EXISTS = &H8000400F
        CO_E_INIT_SCM_MAP_VIEW_OF_FILE = &H80004010
        CO_E_INIT_SCM_EXEC_FAILURE = &H80004011
        CO_E_INIT_ONLY_SINGLE_THREADED = &H80004012
        S_OK = &H0
        S_FALSE = &H1
        OLE_E_FIRST = &H80040000
        OLE_E_LAST = &H800400FF
        OLE_S_FIRST = &H40000
        OLE_S_LAST = &H400FF
        OLE_E_OLEVERB = &H80040000
        OLE_E_ADVF = &H80040001
        OLE_E_ENUM_NOMORE = &H80040002
        OLE_E_ADVISENOTSUPPORTED = &H80040003
        OLE_E_NOCONNECTION = &H80040004
        OLE_E_NOTRUNNING = &H80040005
        OLE_E_NOCACHE = &H80040006
        OLE_E_BLANK = &H80040007
        OLE_E_CLASSDIFF = &H80040008
        OLE_E_CANT_GETMONIKER = &H80040009
        OLE_E_CANT_BINDTOSOURCE = &H8004000A
        OLE_E_STATIC = &H8004000B
        OLE_E_PROMPTSAVECANCELLED = &H8004000C
        OLE_E_INVALIDRECT = &H8004000D
        OLE_E_WRONGCOMPOBJ = &H8004000E
        OLE_E_INVALIDHWND = &H8004000F
        OLE_E_NOT_INPLACEACTIVE = &H80040010
        OLE_E_CANTCONVERT = &H80040011
        OLE_E_NOSTORAGE = &H80040012
        DV_E_FORMATETC = &H80040064
        DV_E_DVTARGETDEVICE = &H80040065
        DV_E_STGMEDIUM = &H80040066
        DV_E_STATDATA = &H80040067
        DV_E_LINDEX = &H80040068
        DV_E_TYMED = &H80040069
        DV_E_CLIPFORMAT = &H8004006A
        DV_E_DVASPECT = &H8004006B
        DV_E_DVTARGETDEVICE_SIZE = &H8004006C
        DV_E_NOIVIEWOBJECT = &H8004006D
        DRAGDROP_E_FIRST = &H80040100
        DRAGDROP_E_LAST = &H8004010F
        DRAGDROP_S_FIRST = &H40100
        DRAGDROP_S_LAST = &H4010F
        DRAGDROP_E_NOTREGISTERED = &H80040100
        DRAGDROP_E_ALREADYREGISTERED = &H80040101
        DRAGDROP_E_INVALIDHWND = &H80040102
        CLASSFACTORY_E_FIRST = &H80040110
        CLASSFACTORY_E_LAST = &H8004011F
        CLASSFACTORY_S_FIRST = &H40110
        CLASSFACTORY_S_LAST = &H4011F
        CLASS_E_NOAGGREGATION = &H80040110
        CLASS_E_CLASSNOTAVAILABLE = &H80040111
        MARSHAL_E_FIRST = &H80040120
        MARSHAL_E_LAST = &H8004012F
        MARSHAL_S_FIRST = &H40120
        MARSHAL_S_LAST = &H4012F
        DATA_E_FIRST = &H80040130
        DATA_E_LAST = &H8004013F
        DATA_S_FIRST = &H40130
        DATA_S_LAST = &H4013F
        VIEW_E_FIRST = &H80040140
        VIEW_E_LAST = &H8004014F
        VIEW_S_FIRST = &H40140
        VIEW_S_LAST = &H4014F
        VIEW_E_DRAW = &H80040140
        REGDB_E_FIRST = &H80040150
        REGDB_E_LAST = &H8004015F
        REGDB_S_FIRST = &H40150
        REGDB_S_LAST = &H4015F
        REGDB_E_READREGDB = &H80040150
        REGDB_E_WRITEREGDB = &H80040151
        REGDB_E_KEYMISSING = &H80040152
        REGDB_E_INVALIDVALUE = &H80040153
        REGDB_E_CLASSNOTREG = &H80040154
        REGDB_E_IIDNOTREG = &H80040155
        CACHE_E_FIRST = &H80040170
        CACHE_E_LAST = &H8004017F
        CACHE_S_FIRST = &H40170
        CACHE_S_LAST = &H4017F
        CACHE_E_NOCACHE_UPDATED = &H80040170
        OLEOBJ_E_FIRST = &H80040180
        OLEOBJ_E_LAST = &H8004018F
        OLEOBJ_S_FIRST = &H40180
        OLEOBJ_S_LAST = &H4018F
        OLEOBJ_E_NOVERBS = &H80040180
        OLEOBJ_E_INVALIDVERB = &H80040181
        CLIENTSITE_E_FIRST = &H80040190
        CLIENTSITE_E_LAST = &H8004019F
        CLIENTSITE_S_FIRST = &H40190
        CLIENTSITE_S_LAST = &H4019F
        INPLACE_E_NOTUNDOABLE = &H800401A0
        INPLACE_E_NOTOOLSPACE = &H800401A1
        INPLACE_E_FIRST = &H800401A0
        INPLACE_E_LAST = &H800401AF
        INPLACE_S_FIRST = &H401A0
        INPLACE_S_LAST = &H401AF
        ENUM_E_FIRST = &H800401B0
        ENUM_E_LAST = &H800401BF
        ENUM_S_FIRST = &H401B0
        ENUM_S_LAST = &H401BF
        CONVERT10_E_FIRST = &H800401C0
        CONVERT10_E_LAST = &H800401CF
        CONVERT10_S_FIRST = &H401C0
        CONVERT10_S_LAST = &H401CF
        CONVERT10_E_OLESTREAM_GET = &H800401C0
        CONVERT10_E_OLESTREAM_PUT = &H800401C1
        CONVERT10_E_OLESTREAM_FMT = &H800401C2
        CONVERT10_E_OLESTREAM_BITMAP_TO_DIB = &H800401C3
        CONVERT10_E_STG_FMT = &H800401C4
        CONVERT10_E_STG_NO_STD_STREAM = &H800401C5
        CONVERT10_E_STG_DIB_TO_BITMAP = &H800401C6
        CLIPBRD_E_FIRST = &H800401D0
        CLIPBRD_E_LAST = &H800401DF
        CLIPBRD_S_FIRST = &H401D0
        CLIPBRD_S_LAST = &H401DF
        CLIPBRD_E_CANT_OPEN = &H800401D0
        CLIPBRD_E_CANT_EMPTY = &H800401D1
        CLIPBRD_E_CANT_SET = &H800401D2
        CLIPBRD_E_BAD_DATA = &H800401D3
        CLIPBRD_E_CANT_CLOSE = &H800401D4
        MK_E_FIRST = &H800401E0
        MK_E_LAST = &H800401EF
        MK_S_FIRST = &H401E0
        MK_S_LAST = &H401EF
        MK_E_CONNECTMANUALLY = &H800401E0
        MK_E_EXCEEDEDDEADLINE = &H800401E1
        MK_E_NEEDGENERIC = &H800401E2
        MK_E_UNAVAILABLE = &H800401E3
        MK_E_SYNTAX = &H800401E4
        MK_E_NOOBJECT = &H800401E5
        MK_E_INVALIDEXTENSION = &H800401E6
        MK_E_INTERMEDIATEINTERFACENOTSUPPORTED = &H800401E7
        MK_E_NOTBINDABLE = &H800401E8
        MK_E_NOTBOUND = &H800401E9
        MK_E_CANTOPENFILE = &H800401EA
        MK_E_MUSTBOTHERUSER = &H800401EB
        MK_E_NOINVERSE = &H800401EC
        MK_E_NOSTORAGE = &H800401ED
        MK_E_NOPREFIX = &H800401EE
        MK_E_ENUMERATION_FAILED = &H800401EF
        CO_E_FIRST = &H800401F0
        CO_E_LAST = &H800401FF
        CO_S_FIRST = &H401F0
        CO_S_LAST = &H401FF
        CO_E_NOTINITIALIZED = &H800401F0
        CO_E_ALREADYINITIALIZED = &H800401F1
        CO_E_CANTDETERMINECLASS = &H800401F2
        CO_E_CLASSSTRING = &H800401F3
        CO_E_IIDSTRING = &H800401F4
        CO_E_APPNOTFOUND = &H800401F5
        CO_E_APPSINGLEUSE = &H800401F6
        CO_E_ERRORINAPP = &H800401F7
        CO_E_DLLNOTFOUND = &H800401F8
        CO_E_ERRORINDLL = &H800401F9
        CO_E_WRONGOSFORAPP = &H800401FA
        CO_E_OBJNOTREG = &H800401FB
        CO_E_OBJISREG = &H800401FC
        CO_E_OBJNOTCONNECTED = &H800401FD
        CO_E_APPDIDNTREG = &H800401FE
        CO_E_RELEASED = &H800401FF
        OLE_S_USEREG = &H40000
        OLE_S_STATIC = &H40001
        OLE_S_MAC_CLIPFORMAT = &H40002
        DRAGDROP_S_DROP = &H40100
        DRAGDROP_S_CANCEL = &H40101
        DRAGDROP_S_USEDEFAULTCURSORS = &H40102
        DATA_S_SAMEFORMATETC = &H40130
        VIEW_S_ALREADY_FROZEN = &H40140
        CACHE_S_FORMATETC_NOTSUPPORTED = &H40170
        CACHE_S_SAMECACHE = &H40171
        CACHE_S_SOMECACHES_NOTUPDATED = &H40172
        OLEOBJ_S_INVALIDVERB = &H40180
        OLEOBJ_S_CANNOT_DOVERB_NOW = &H40181
        OLEOBJ_S_INVALIDHWND = &H40182
        INPLACE_S_TRUNCATED = &H401A0
        CONVERT10_S_NO_PRESENTATION = &H401C0
        MK_S_REDUCED_TO_SELF = &H401E2
        MK_S_ME = &H401E4
        MK_S_HIM = &H401E5
        MK_S_US = &H401E6
        MK_S_MONIKERALREADYREGISTERED = &H401E7
        CO_E_CLASS_CREATE_FAILED = &H80080001
        CO_E_SCM_ERROR = &H80080002
        CO_E_SCM_RPC_FAILURE = &H80080003
        CO_E_BAD_PATH = &H80080004
        CO_E_SERVER_EXEC_FAILURE = &H80080005
        CO_E_OBJSRV_RPC_FAILURE = &H80080006
        MK_E_NO_NORMALIZED = &H80080007
        CO_E_SERVER_STOPPING = &H80080008
        MEM_E_INVALID_ROOT = &H80080009
        MEM_E_INVALID_LINK = &H80080010
        MEM_E_INVALID_SIZE = &H80080011
        DISP_E_UNKNOWNINTERFACE = &H80020001
        DISP_E_MEMBERNOTFOUND = &H80020003
        DISP_E_PARAMNOTFOUND = &H80020004
        DISP_E_TYPEMISMATCH = &H80020005
        DISP_E_UNKNOWNNAME = &H80020006
        DISP_E_NONAMEDARGS = &H80020007
        DISP_E_BADVARTYPE = &H80020008
        DISP_E_EXCEPTION = &H80020009
        DISP_E_OVERFLOW = &H8002000A
        DISP_E_BADINDEX = &H8002000B
        DISP_E_UNKNOWNLCID = &H8002000C
        DISP_E_ARRAYISLOCKED = &H8002000D
        DISP_E_BADPARAMCOUNT = &H8002000E
        DISP_E_PARAMNOTOPTIONAL = &H8002000F
        DISP_E_BADCALLEE = &H80020010
        DISP_E_NOTACOLLECTION = &H80020011
        TYPE_E_BUFFERTOOSMALL = &H80028016
        TYPE_E_INVDATAREAD = &H80028018
        TYPE_E_UNSUPFORMAT = &H80028019
        TYPE_E_REGISTRYACCESS = &H8002801C
        TYPE_E_LIBNOTREGISTERED = &H8002801D
        TYPE_E_UNDEFINEDTYPE = &H80028027
        TYPE_E_QUALIFIEDNAMEDISALLOWED = &H80028028
        TYPE_E_INVALIDSTATE = &H80028029
        TYPE_E_WRONGTYPEKIND = &H8002802A
        TYPE_E_ELEMENTNOTFOUND = &H8002802B
        TYPE_E_AMBIGUOUSNAME = &H8002802C
        TYPE_E_NAMECONFLICT = &H8002802D
        TYPE_E_UNKNOWNLCID = &H8002802E
        TYPE_E_DLLFUNCTIONNOTFOUND = &H8002802F
        TYPE_E_BADMODULEKIND = &H800288BD
        TYPE_E_SIZETOOBIG = &H800288C5
        TYPE_E_DUPLICATEID = &H800288C6
        TYPE_E_INVALIDID = &H800288CF
        TYPE_E_TYPEMISMATCH = &H80028CA0
        TYPE_E_OUTOFBOUNDS = &H80028CA1
        TYPE_E_IOERROR = &H80028CA2
        TYPE_E_CANTCREATETMPFILE = &H80028CA3
        TYPE_E_CANTLOADLIBRARY = &H80029C4A
        TYPE_E_INCONSISTENTPROPFUNCS = &H80029C83
        TYPE_E_CIRCULARTYPE = &H80029C84
        STG_E_INVALIDFUNCTION = &H80030001
        STG_E_FILENOTFOUND = &H80030002
        STG_E_PATHNOTFOUND = &H80030003
        STG_E_TOOMANYOPENFILES = &H80030004
        STG_E_ACCESSDENIED = &H80030005
        STG_E_INVALIDHANDLE = &H80030006
        STG_E_INSUFFICIENTMEMORY = &H80030008
        STG_E_INVALIDPOINTER = &H80030009
        STG_E_NOMOREFILES = &H80030012
        STG_E_DISKISWRITEPROTECTED = &H80030013
        STG_E_SEEKERROR = &H80030019
        STG_E_WRITEFAULT = &H8003001D
        STG_E_READFAULT = &H8003001E
        STG_E_SHAREVIOLATION = &H80030020
        STG_E_LOCKVIOLATION = &H80030021
        STG_E_FILEALREADYEXISTS = &H80030050
        STG_E_INVALIDPARAMETER = &H80030057
        STG_E_MEDIUMFULL = &H80030070
        STG_E_ABNORMALAPIEXIT = &H800300FA
        STG_E_INVALIDHEADER = &H800300FB
        STG_E_INVALIDNAME = &H800300FC
        STG_E_UNKNOWN = &H800300FD
        STG_E_UNIMPLEMENTEDFUNCTION = &H800300FE
        STG_E_INVALIDFLAG = &H800300FF
        STG_E_INUSE = &H80030100
        STG_E_NOTCURRENT = &H80030101
        STG_E_REVERTED = &H80030102
        STG_E_CANTSAVE = &H80030103
        STG_E_OLDFORMAT = &H80030104
        STG_E_OLDDLL = &H80030105
        STG_E_SHAREREQUIRED = &H80030106
        STG_E_NOTFILEBASEDSTORAGE = &H80030107
        STG_E_EXTANTMARSHALLINGS = &H80030108
        STG_S_CONVERTED = &H30200
        RPC_E_CALL_REJECTED = &H80010001
        RPC_E_CALL_CANCELED = &H80010002
        RPC_E_CANTPOST_INSENDCALL = &H80010003
        RPC_E_CANTCALLOUT_INASYNCCALL = &H80010004
        RPC_E_CANTCALLOUT_INEXTERNALCALL = &H80010005
        RPC_E_CONNECTION_TERMINATED = &H80010006
        RPC_E_SERVER_DIED = &H80010007
        RPC_E_CLIENT_DIED = &H80010008
        RPC_E_INVALID_DATAPACKET = &H80010009
        RPC_E_CANTTRANSMIT_CALL = &H8001000A
        RPC_E_CLIENT_CANTMARSHAL_DATA = &H8001000B
        RPC_E_CLIENT_CANTUNMARSHAL_DATA = &H8001000C
        RPC_E_SERVER_CANTMARSHAL_DATA = &H8001000D
        RPC_E_SERVER_CANTUNMARSHAL_DATA = &H8001000E
        RPC_E_INVALID_DATA = &H8001000F
        RPC_E_INVALID_PARAMETER = &H80010010
        RPC_E_CANTCALLOUT_AGAIN = &H80010011
        RPC_E_SERVER_DIED_DNE = &H80010012
        RPC_E_SYS_CALL_FAILED = &H80010100
        RPC_E_OUT_OF_RESOURCES = &H80010101
        RPC_E_ATTEMPTED_MULTITHREAD = &H80010102
        RPC_E_NOT_REGISTERED = &H80010103
        RPC_E_FAULT = &H80010104
        RPC_E_SERVERFAULT = &H80010105
        RPC_E_CHANGED_MODE = &H80010106
        RPC_E_INVALIDMETHOD = &H80010107
        RPC_E_DISCONNECTED = &H80010108
        RPC_E_RETRY = &H80010109
        RPC_E_SERVERCALL_RETRYLATER = &H8001010A
        RPC_E_SERVERCALL_REJECTED = &H8001010B
        RPC_E_INVALID_CALLDATA = &H8001010C
        RPC_E_CANTCALLOUT_ININPUTSYNCCALL = &H8001010D
        RPC_E_WRONG_THREAD = &H8001010E
        RPC_E_THREAD_NOT_INIT = &H8001010F
        RPC_E_UNEXPECTED = &H8001FFFF
    End Enum

    Public Function Connect(ByVal Server As String, ByVal UserName As String, ByVal UserPassword As String, ByVal AsDrive As String) As String
        Dim eResult As ErrorCodes

        _UserName = UserName
        _UserPassword = UserPassword
        _NetzResource.lpRemoteName = Server
        _NetzResource.lpLocalName = AsDrive
        _NetzResource.dwType = RESOURCETYPE_DISK

        Try

            ' Hier wird verbunden
            ' dwFlag muss 1 sein
            eResult = CType(WNetAddConnection2(_NetzResource, _UserPassword, _UserName, 1), ErrorCodes)

            If eResult = ErrorCodes.NO_ERROR Or eResult = ErrorCodes.NOERROR Then
                Return ""
            End If
            Return eResult.ToString

        Catch ex As Exception
            Return ex.Message

        End Try

    End Function

    Public Function Disconnect(ByVal Drive As String) As String
        Dim eResult As ErrorCodes
        Try

            _NetzResource.lpLocalName = Drive
            eResult = CType(WNetCancelConnection2(Drive, 0, -1), ErrorCodes)

            If eResult = ErrorCodes.NO_ERROR Or eResult = ErrorCodes.NOERROR Then
                Return ""
            End If
            Return eResult.ToString

        Catch ex As Exception
            Return ex.Message

        End Try

    End Function



End Class

