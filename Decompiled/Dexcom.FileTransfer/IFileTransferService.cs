// Type: Dexcom.FileTransfer.IFileTransferService
// Assembly: Dexcom.FileTransfer, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 38934138-A845-4F5C-AA0D-8047C5BBDF07
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.FileTransfer.dll

using Dexcom.Common.Data;

namespace Dexcom.FileTransfer
{
  public interface IFileTransferService
  {
    XFileTransfer BeginFileTransferToServer(XOnlineContext context, XFileTransfer xFileTransfer);

    XFileTransfer SendFileToServer(XOnlineContext context, XFileTransfer xFileTransfer);

    XFileTransfer EndFileTransferToServer(XOnlineContext context, XFileTransfer xFileTransfer);

    XFileTransfer AbortFileTransferToServer(XOnlineContext context, XFileTransfer xFileTransfer);

    XFileTransfer BeginFileTransferToClient(XOnlineContext context, XFileTransfer xFileTransfer);

    XFileTransfer SendFileToClient(XOnlineContext context, XFileTransfer xFileTransfer);

    XFileTransfer EndFileTransferToClient(XOnlineContext context, XFileTransfer xFileTransfer);

    XFileTransfer AbortFileTransferToClient(XOnlineContext context, XFileTransfer xFileTransfer);
  }
}
