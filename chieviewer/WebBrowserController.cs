using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Security.Permissions;




namespace chieviewer
{

    [ComVisible(true)]
    public class WebBrowserController : Control, IOleClientSite
    {

        private const int DISPID_AMBIENT_DLCONTROL = -5512;
        private WebBrowser _WebBrowser;

        private DLCTL _DlControl = DLCTL.DLIMAGES | DLCTL.BGSOUNDS | DLCTL.VIDEOS;
        public WebBrowserController(WebBrowser WebBrowser)
        {
            this._WebBrowser = WebBrowser;
            ((IOleObject)this._WebBrowser.ActiveXInstance).SetClientSite(this);
        }


        [DispId(DISPID_AMBIENT_DLCONTROL)]
        public int Didpid_Ambient_DlControl()
        {
            return (int)this._DlControl;
        }

        private void OnAmbientPropertyChange()
        {
            ((IOleControl)((SHDocVw.IWebBrowser2)this._WebBrowser.ActiveXInstance).Application).OnAmbientPropertyChange(DISPID_AMBIENT_DLCONTROL);
        }

        public DLCTL DlControl
        {
            get { return this._DlControl; }
            set
            {
                this._DlControl = value;
                this.OnAmbientPropertyChange();
                this._WebBrowser.Refresh();
            }
        }

        public void GetContainer(ref object ppContainer)
        {
        }
        public void GetMoniker(int dwAssign, int dwWhichMoniker, ref object ppmk)
        {
        }
        public void OnShowWindow(bool fShow)
        {
        }
        public void RequestNewObjectLayout()
        {
        }
        public void SaveObject()
        {
        }
        public void ShowObject()
        {
        }

    }

    public enum DLCTL : uint
    {
        BGSOUNDS = 0x40,
        //BGMを再生する
        DLIMAGES = 0x10,
        //サーバーから画像をダウンロードする
        DOWNLOADONLY = 0x800,
        //コンポーネントをダウンロードするが表示しない
        FORCEOFFLINE = 0x10000000,
        //常にオフラインモード
        NO_BEHAVIORS = 0x8000,
        NO_CLIENTPULL = 0x20000000,
        NO_DLACTIVEXCTLS = 0x400,
        //ActiveXコントロールをダウンロードしない
        NO_FRAMEDOWNLOAD = 0x1000,
        //フレームをダウンロードしない
        NO_JAVA = 0x100,
        //JAVAアプレットを実行しない
        NO_METACHARSET = 0x10000,
        NO_RUNACTIVEXCTLS = 0x200,
        //ActiveXコントロールを実行しない
        NO_SCRIPTS = 0x80,
        //スクリプトを実行しない
        OFFLINE = 0x80000000,
        OFFLINEIFNOTCONNECTED = 0x80000000,
        PRAGMA_NO_CACHE = 0x4000,
        RESYNCHRONIZE = 0x2000,
        SILENT = 0x40000000,
        //ダイアログを表示しない
        URL_ENCODING_DISABLE_UTF8 = 0x20000,
        URL_ENCODING_ENABLE_UTF8 = 0x40000,
        VIDEOS = 0x20
        //ビデオクリップを再生する
    }

    [GuidAttribute("B196B288-BAB4-101A-B69C-00AA00341D07"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleControl
    {

        void GetControlInfo(ref object pCI);
        void OnMnemonic(ref object pMsg);
        void OnAmbientPropertyChange(int dispID);

        void FreezeEvents(bool bFreeze);
    }

    [Guid("00000118-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleClientSite
    {

        void SaveObject();
        void GetMoniker(int dwAssign, int dwWhichMoniker, ref object ppmk);
        void GetContainer(ref object ppContainer);
        void ShowObject();
        void OnShowWindow(bool fShow);

        void RequestNewObjectLayout();
    }

    [Guid("00000112-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleObject
    {

        void SetClientSite(IOleClientSite pClientSite);
        void GetClientSite(ref IOleClientSite ppClientSite);
        void SetHostNames(object szContainerApp, object szContainerObj);
        void Close(int dwSaveOption);
        void SetMoniker(int dwWhichMoniker, object pmk);
        void GetMoniker(int dwAssign, int dwWhichMoniker, object ppmk);
        void InitFromData(IDataObject pDataObject, bool fCreation, int dwReserved);
        void GetClipboardData(int dwReserved, ref IDataObject ppDataObject);
        void DoVerb(int iVerb, int lpmsg, object pActiveSite, int lindex, int hwndParent, int lprcPosRect);
        void EnumVerbs(ref object ppEnumOleVerb);
        void Update();
        void IsUpToDate();
        void GetUserClassID(int pClsid);
        void GetUserType(int dwFormOfType, int pszUserType);
        void SetExtent(int dwDrawAspect, int psizel);
        void GetExtent(int dwDrawAspect, int psizel);
        void Advise(object pAdvSink, int pdwConnection);
        void Unadvise(int dwConnection);
        void EnumAdvise(ref object ppenumAdvise);
        void GetMiscStatus(int dwAspect, int pdwStatus);

        void SetColorScheme(object pLogpal);
    }

}


