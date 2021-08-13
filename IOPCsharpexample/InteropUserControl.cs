using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Drawing;
using CefSharp;
using CefSharp.WinForms;

namespace IOPCsharpexample
{

    #region Interfaces

    [ComVisible(true), Guid(InteropUserControl.EventsId), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface __InteropUserControl
    {
        [DispId(1)]
        void Click();
        [DispId(2)]
        void DblClick();
        //add additional events visible in VB6
        // these events exposed and shown in PB event list
        [DispId(3)]
        void tbScroll();
        [DispId(4)]
        void tbValuechanged();
    }

    [Guid(InteropUserControl.InterfaceId), ComVisible(true)]
    public interface _InteropUserControl
    {
        [DispId(1)]
        bool Visible { [DispId(1)] get; [DispId(1)] set; }
        [DispId(2)]
        bool Enabled { [DispId(2)] get; [DispId(2)] set; }
        [DispId(3)]
        int ForegroundColor { [DispId(3)] get; [DispId(3)] set; }
        [DispId(4)]
        int BackgroundColor { [DispId(4)] get; [DispId(4)] set; }
        [DispId(5)]
        Image BackgroundImage { [DispId(5)] get; [DispId(5)] set; }
        [DispId(6)]
        void Refresh();
        //add additional properties visible in VB6
        // PB can call these
        [DispId(7)]
        void tbsetbackcolor(int testval);
        [DispId(8)]
        int tb1backgroundcolor { [DispId(8)] get; [DispId(8)] set; }
        [DispId(9)]
        int tb1value { [DispId(9)] get; [DispId(9)] set; }
        [DispId(10)]
        void initializechromium();
        [DispId(11)]
        void navigate(string url);
    }
    #endregion

    [Guid(InteropUserControl.ClassId), ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces("IOPCsharpexample.__InteropUserControl")]
    [ComClass(InteropUserControl.ClassId, InteropUserControl.InterfaceId, InteropUserControl.EventsId)]
    public partial class InteropUserControl : UserControl, _InteropUserControl
    {
        #region VB6 Interop Code

#if COM_INTEROP_ENABLED

        #region "COM Registration"

        //These  GUIDs provide the COM identity for this class 
        //and its COM interfaces. If you change them, existing 
        //clients will no longer be able to access the class.

        public const string ClassId = "e7c6c97a-af38-4d57-9980-9edd60e1b45c";
        public const string InterfaceId = "2aab63cc-a9df-4197-89f4-44150a746301";
        public const string EventsId = "1eeca2b9-97ac-4c1c-8dc1-770c941c8ebd";

        //These routines perform the additional COM registration needed by ActiveX controls
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComRegisterFunction]
        private static void Register(System.Type t)
        {
            ComRegistration.RegisterControl(t);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComUnregisterFunction]
        private static void Unregister(System.Type t)
        {
            ComRegistration.UnregisterControl(t);
        }


        #endregion

        #region "VB6 Events"

        //This section shows some examples of exposing a UserControl's events to VB6.  Typically, you just
        //1) Declare the event as you want it to be shown in VB6
        //2) Raise the event in the appropriate UserControl event.
        public delegate void ClickEventHandler();
        public delegate void DblClickEventHandler();
        public new event ClickEventHandler Click; //Event must be marked as new since .NET UserControls have the same name.
        public event DblClickEventHandler DblClick;

        private void InteropUserControl_Click(object sender, System.EventArgs e)
        {
            if (null != Click)
                Click();
        }

        private void InteropUserControl_DblClick(object sender, System.EventArgs e)
        {
            if (null != DblClick)
                DblClick();
        }


        #endregion

        #region "VB6 Properties"

        //The following are examples of how to expose typical form properties to VB6.  
        //You can also use these as examples on how to add additional properties.

        //Must Shadow this property as it exists in Windows.Forms and is not overridable
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public new bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = value; }
        }

        public int ForegroundColor
        {
            get 
            {
                return ActiveXControlHelpers.GetOleColorFromColor(base.ForeColor);
            }
            set
            {
                base.ForeColor = ActiveXControlHelpers.GetColorFromOleColor(value);
            }
        }

        public int BackgroundColor
        {
            get
            {
                return ActiveXControlHelpers.GetOleColorFromColor(base.BackColor);
                }
            set
            {
                base.BackColor = ActiveXControlHelpers.GetColorFromOleColor(value);
            }
        }

        public override System.Drawing.Image BackgroundImage
        {
            get{return null;}
            set
            {
                if(null != value)
                {
                    MessageBox.Show("Setting the background image of an Interop UserControl is not supported, please use a PictureBox instead.", "Information");
                }
                base.BackgroundImage = null;
            }
        }

        #endregion

        #region "VB6 Methods"

            public override void Refresh()
            {
                base.Refresh();
            }

            //Ensures that tabbing across VB6 and .NET controls works as expected
            private void InteropUserControl_LostFocus(object sender, System.EventArgs e)
            {
                ActiveXControlHelpers.HandleFocus(this);
            }

            public InteropUserControl()
            {
                //This call is required by the Windows Form Designer.
                InitializeComponent();

                //' Add any initialization after the InitializeComponent() call.
                this.DoubleClick += new System.EventHandler(this.InteropUserControl_DblClick);
                base.Click += new System.EventHandler(this.InteropUserControl_Click);
                this.LostFocus += new System.EventHandler(InteropUserControl_LostFocus); 
                this.ControlAdded += new ControlEventHandler(InteropUserControl_ControlAdded);
                // new events to expose to PB
                this.TB1.ValueChanged += new System.EventHandler(TB1_ValueChanged);
                this.TB1.Scroll += new System.EventHandler(TB1_Scroll);
                //////////
                //'Raise custom Load event
                this.OnCreateControl();
            }

            [SecurityPermission(SecurityAction.LinkDemand, Flags =SecurityPermissionFlag.UnmanagedCode)]
            protected override void WndProc(ref System.Windows.Forms.Message m)
            {

                const int WM_SETFOCUS = 0x7;
                const int WM_PARENTNOTIFY = 0x210;
                const int WM_DESTROY = 0x2;
                const int WM_LBUTTONDOWN = 0x201;
                const int WM_RBUTTONDOWN = 0x204;

                if (m.Msg == WM_SETFOCUS)
                {
                    //Raise Enter event
                    this.OnEnter(System.EventArgs.Empty);
                }
                else if( m.Msg == WM_PARENTNOTIFY && (m.WParam.ToInt32() == WM_LBUTTONDOWN || m.WParam.ToInt32() == WM_RBUTTONDOWN))
                {

                    if (!this.ContainsFocus)
                    {
                        //Raise Enter event
                        this.OnEnter(System.EventArgs.Empty);
                    }
                }
                else if (m.Msg == WM_DESTROY && !this.IsDisposed && !this.Disposing)
                {
                    //Used to ensure that VB6 will cleanup control properly
                    this.Dispose();
                }

                base.WndProc(ref m);
            }


            //This event will hook up the necessary handlers
            private void InteropUserControl_ControlAdded(object sender, ControlEventArgs e)
            {
                ActiveXControlHelpers.WireUpHandlers(e.Control, ValidationHandler);
            }

            //Ensures that the Validating and Validated events fire appropriately
            internal void ValidationHandler(object sender, System.EventArgs e)
            {
                if( this.ContainsFocus) return;

                //Raise Leave event
                this.OnLeave(e);

                if (this.CausesValidation)
                {
                    CancelEventArgs validationArgs = new CancelEventArgs();
                    this.OnValidating(validationArgs);

                    if(validationArgs.Cancel && this.ActiveControl != null)
                        this.ActiveControl.Focus();
                    else
                    {
                        //Raise Validated event
                        this.OnValidated(e);
                    }
                }

            }

        #endregion

#endif

        #endregion

        //Please enter any new code here, below the Interop code
        // events to expose to PB
        public delegate void tbValueChangedEventHandler();
        public event tbValueChangedEventHandler tbValuechanged;
        private void TB1_ValueChanged(object sender, System.EventArgs e)
        {
            if (tbValuechanged != null)
            {
                tbValuechanged();
            }
        }

        public event tbScrollEventHandler tbScroll;
        public delegate void tbScrollEventHandler();
        private void TB1_Scroll(object sender, System.EventArgs e)
        {
            if (tbScroll != null)
            {
                tbScroll();
            }
        }
        ////////////
        //method to call from PB
        public void tbsetbackcolor(int testval)
        {
            Color myColor = ColorTranslator.FromWin32(testval);

            TB1.BackColor = myColor;
            BackColor = myColor;
        }
        ///////////
        // Property get/set to call from PB
        public int tb1backgroundcolor
        {
            get { return ActiveXControlHelpers.GetOleColorFromColor(TB1.BackColor); }
            set { TB1.BackColor = ActiveXControlHelpers.GetColorFromOleColor(value); }
        }
        public int tb1value
        {
            get { return TB1.Value; }
            set { TB1.Value = value; }
        }

        public void initializechromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.EnableHighDPISupport();
            Cef.Initialize(settings);
            chromiumWebBrowser1.Dock = DockStyle.Fill;
        }

        public void navigate(string url)
        {
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Load(url);

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            chromiumWebBrowser1.Load("www.google.com");
        }
        ///////////


    }
}
