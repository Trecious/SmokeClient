using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomkeClient
{
    //Thanks too: https://github.com/SteamRE/SteamKit/blob/d9a1da3513e2747dbe2c3d752aebf7464e2d900b/Resources/NetHook2/NetHook2/steam/steamtypes.h

    public enum EResult
    {
        Ok = 1,                            // success
        Fail = 2,                          // generic failure 
        NoConnection = 3,                  // no/failed network connection
        InvalidPassword = 5,               // password/ticket is invalid
        LoggedInElsewhere = 6,             // same user logged in elsewhere
        InvalidProtocolVer = 7,            // protocol version is incorrect
        InvalidParam = 8,                  // a parameter is incorrect
        FileNotFound = 9,                  // file was not found
        Busy = 10,                         // called method busy - action not taken
        InvalidState = 11,                 // called object was in an invalid state
        InvalidName = 12,                  // name is invalid
        InvalidEmail = 13,                 // email is invalid
        DuplicateName = 14,                // name is not unique
        AccessDenied = 15,                 // access is denied
        Timeout = 16,                      // operation timed out
        Banned = 17,                       // VAC2 banned
        AccountNotFound = 18,              // account not found
        InvalidSteamId = 19,               // steamID is invalid
        ServiceUnavailable = 20,           // The requested service is currently unavailable
        NotLoggedOn = 21,                  // The user is not logged on
        Pending = 22,                      // Request is pending (may be in process, or waiting on third party)
        EncryptionFailure = 23,            // Encryption or Decryption failed
        InsufficientPrivilege = 24,        // Insufficient privilege
        LimitExceeded = 25,                // Too much of a good thing
        Revoked = 26,                      // Access has been revoked (used for revoked guest passes)
        Expired = 27,                      // License/Guest pass the user is trying to access is expired
        AlreadyRedeemed = 28,              // Guest pass has already been redeemed by account, cannot be acked again
        DuplicateRequest = 29,             // The request is a duplicate and the action has already occurred in the past, ignored this time
        AlreadyOwned = 30,                 // All the games in this guest pass redemption request are already owned by the user
        IpNotFound = 31,                   // IP address not found
        PersistFailed = 32,                // failed to write change to the data store
        LockingFailed = 33,                // failed to acquire access lock for this operation
        LogonSessionReplaced = 34,
        ConnectFailed = 35,
        HandshakeFailed = 36,
        IoFailure = 37,
        RemoteDisconnect = 38,
        ShoppingCartNotFound = 39,         // failed to find the shopping cart requested
        Blocked = 40,                      // a user didn't allow it
        Ignored = 41,                      // target is ignoring sender
        NoMatch = 42,                      // nothing matching the request found
        AccountDisabled = 43,
        ServiceReadOnly = 44,              // this service is not accepting content changes right now
        AccountNotFeatured = 45,           // account doesn't have value, so this feature isn't available
        AdministratorOk = 46,              // allowed to take this action, but only because requester is admin
        ContentVersion = 47,               // A Version mismatch in content transmitted within the Steam protocol.
        TryAnotherCm = 48,                 // The current CM can't service the user making a request, user should try another.
        PasswordRequiredToKickSession = 49,        // You are already logged in elsewhere, this cached credential login has failed.
        AlreadyLoggedInElsewhere = 50,     // You are already logged in elsewhere, you must wait
        Suspended = 51,
        Cancelled = 52,
        DataCorruption = 53,
        DiskFull = 54,
        RemoteCallFailed = 55,
    };
}
