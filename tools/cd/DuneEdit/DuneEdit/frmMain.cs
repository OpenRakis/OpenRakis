using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DuneEdit.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DuneEdit
{
	[DesignerGenerated]
	public class frmMain : Form
	{
		private static List<WeakReference> __ENCList = new List<WeakReference>();

		private IContainer components;

		[AccessedThroughProperty("lsSietches")]
		private ListBox _lsSietches;

		[AccessedThroughProperty("OpenFileDialog")]
		private OpenFileDialog _OpenFileDialog;

		[AccessedThroughProperty("txtRegion")]
		private TextBox _txtRegion;

		[AccessedThroughProperty("txtSubRegion")]
		private TextBox _txtSubRegion;

		[AccessedThroughProperty("TabControl")]
		private TabControl _TabControl;

		[AccessedThroughProperty("tabSietches")]
		private TabPage _tabSietches;

		[AccessedThroughProperty("tabTroops")]
		private TabPage _tabTroops;

		[AccessedThroughProperty("txtSpicefieldID")]
		private TextBox _txtSpicefieldID;

		[AccessedThroughProperty("txtStatus")]
		private TextBox _txtStatus;

		[AccessedThroughProperty("txtHousedTroopID")]
		private TextBox _txtHousedTroopID;

		[AccessedThroughProperty("Label14")]
		private Label _Label14;

		[AccessedThroughProperty("Label13")]
		private Label _Label13;

		[AccessedThroughProperty("Label12")]
		private Label _Label12;

		[AccessedThroughProperty("Label11")]
		private Label _Label11;

		[AccessedThroughProperty("Label10")]
		private Label _Label10;

		[AccessedThroughProperty("Label9")]
		private Label _Label9;

		[AccessedThroughProperty("Label8")]
		private Label _Label8;

		[AccessedThroughProperty("Label7")]
		private Label _Label7;

		[AccessedThroughProperty("Label6")]
		private Label _Label6;

		[AccessedThroughProperty("Label5")]
		private Label _Label5;

		[AccessedThroughProperty("Label4")]
		private Label _Label4;

		[AccessedThroughProperty("Label3")]
		private Label _Label3;

		[AccessedThroughProperty("Label2")]
		private Label _Label2;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		[AccessedThroughProperty("tabGeneral")]
		private TabPage _tabGeneral;

		[AccessedThroughProperty("Label15")]
		private Label _Label15;

		[AccessedThroughProperty("Label16")]
		private Label _Label16;

		[AccessedThroughProperty("txtEquipment")]
		private TextBox _txtEquipment;

		[AccessedThroughProperty("txtJob")]
		private TextBox _txtJob;

		[AccessedThroughProperty("Label17")]
		private Label _Label17;

		[AccessedThroughProperty("Label18")]
		private Label _Label18;

		[AccessedThroughProperty("Label19")]
		private Label _Label19;

		[AccessedThroughProperty("Label20")]
		private Label _Label20;

		[AccessedThroughProperty("Label21")]
		private Label _Label21;

		[AccessedThroughProperty("Label22")]
		private Label _Label22;

		[AccessedThroughProperty("Label23")]
		private Label _Label23;

		[AccessedThroughProperty("Label24")]
		private Label _Label24;

		[AccessedThroughProperty("Label25")]
		private Label _Label25;

		[AccessedThroughProperty("Label26")]
		private Label _Label26;

		[AccessedThroughProperty("lsTroops")]
		private ListBox _lsTroops;

		[AccessedThroughProperty("txtNextTroopID")]
		private TextBox _txtNextTroopID;

		[AccessedThroughProperty("txtTroopID")]
		private TextBox _txtTroopID;

		[AccessedThroughProperty("btnSietchUpdate")]
		private Button _btnSietchUpdate;

		[AccessedThroughProperty("btnTroopsUpdate")]
		private Button _btnTroopsUpdate;

		[AccessedThroughProperty("ToolStrip1")]
		private ToolStrip _ToolStrip1;

		[AccessedThroughProperty("Label27")]
		private Label _Label27;

		[AccessedThroughProperty("cmbTroopAtSietch")]
		private ComboBox _cmbTroopAtSietch;

		[AccessedThroughProperty("Label28")]
		private Label _Label28;

		[AccessedThroughProperty("txtSpice")]
		private NumericUpDown _txtSpice;

		[AccessedThroughProperty("txtCharisma")]
		private NumericUpDown _txtCharisma;

		[AccessedThroughProperty("txtContactDistance")]
		private NumericUpDown _txtContactDistance;

		[AccessedThroughProperty("txtWater")]
		private NumericUpDown _txtWater;

		[AccessedThroughProperty("txtBulbs")]
		private NumericUpDown _txtBulbs;

		[AccessedThroughProperty("txtAtomics")]
		private NumericUpDown _txtAtomics;

		[AccessedThroughProperty("txtWeirding")]
		private NumericUpDown _txtWeirding;

		[AccessedThroughProperty("txtLaserGuns")]
		private NumericUpDown _txtLaserGuns;

		[AccessedThroughProperty("txtKrys")]
		private NumericUpDown _txtKrys;

		[AccessedThroughProperty("txtOrni")]
		private NumericUpDown _txtOrni;

		[AccessedThroughProperty("txtHarvesters")]
		private NumericUpDown _txtHarvesters;

		[AccessedThroughProperty("txtSpiceDensity")]
		private NumericUpDown _txtSpiceDensity;

		[AccessedThroughProperty("gbStatus")]
		private GroupBox _gbStatus;

		[AccessedThroughProperty("chkSietchStatus6")]
		private CheckBox _chkSietchStatus6;

		[AccessedThroughProperty("chkSietchStatus5")]
		private CheckBox _chkSietchStatus5;

		[AccessedThroughProperty("chkSietchStatus3")]
		private CheckBox _chkSietchStatus3;

		[AccessedThroughProperty("chkSietchStatus4")]
		private CheckBox _chkSietchStatus4;

		[AccessedThroughProperty("chkSietchStatus2")]
		private CheckBox _chkSietchStatus2;

		[AccessedThroughProperty("chkSietchStatus1")]
		private CheckBox _chkSietchStatus1;

		[AccessedThroughProperty("chkSietchStatus8")]
		private CheckBox _chkSietchStatus8;

		[AccessedThroughProperty("chkSietchStatus7")]
		private CheckBox _chkSietchStatus7;

		[AccessedThroughProperty("GroupBox1")]
		private GroupBox _GroupBox1;

		[AccessedThroughProperty("tabMap")]
		private TabPage _tabMap;

		[AccessedThroughProperty("lblJobDesc")]
		private Label _lblJobDesc;

		[AccessedThroughProperty("txtPopulation")]
		private NumericUpDown _txtPopulation;

		[AccessedThroughProperty("txtEcologySkill")]
		private NumericUpDown _txtEcologySkill;

		[AccessedThroughProperty("txtArmySkill")]
		private NumericUpDown _txtArmySkill;

		[AccessedThroughProperty("txtMotivation")]
		private NumericUpDown _txtMotivation;

		[AccessedThroughProperty("txtSpiceSkill")]
		private NumericUpDown _txtSpiceSkill;

		[AccessedThroughProperty("txtDissatisfaction")]
		private NumericUpDown _txtDissatisfaction;

		[AccessedThroughProperty("chkHarverster")]
		private CheckBox _chkHarverster;

		[AccessedThroughProperty("chkOrni")]
		private CheckBox _chkOrni;

		[AccessedThroughProperty("chkKrys")]
		private CheckBox _chkKrys;

		[AccessedThroughProperty("chkLaserGuns")]
		private CheckBox _chkLaserGuns;

		[AccessedThroughProperty("chkWeirdings")]
		private CheckBox _chkWeirdings;

		[AccessedThroughProperty("chkAtomics")]
		private CheckBox _chkAtomics;

		[AccessedThroughProperty("chkBulb")]
		private CheckBox _chkBulb;

		[AccessedThroughProperty("txtSubRegionDesc")]
		private TextBox _txtSubRegionDesc;

		[AccessedThroughProperty("txtRegionDesc")]
		private TextBox _txtRegionDesc;

		[AccessedThroughProperty("ToolStripDropDownButton1")]
		private ToolStripDropDownButton _ToolStripDropDownButton1;

		[AccessedThroughProperty("OpenToolStripMenuItem")]
		private ToolStripMenuItem _OpenToolStripMenuItem;

		[AccessedThroughProperty("btnSave")]
		private ToolStripMenuItem _btnSave;

		[AccessedThroughProperty("ToolStripSeparator1")]
		private ToolStripSeparator _ToolStripSeparator1;

		[AccessedThroughProperty("ExitToolStripMenuItem")]
		private ToolStripMenuItem _ExitToolStripMenuItem;

		[AccessedThroughProperty("lblAbout")]
		private Label _lblAbout;

		private string FileName;

		private Savegame_Item Savegame;

		private Sietch_List Sietches;

		private Generals General;

		private Troops_List Troops;

		private bool isLoading;

		internal virtual ListBox lsSietches
        {
            [DebuggerNonUserCode]
            get => _lsSietches;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = lsSietches_SelectedIndexChanged;
                if (_lsSietches != null)
                {
                    _lsSietches.SelectedIndexChanged -= value2;
                }
                _lsSietches = value;
                if (_lsSietches != null)
                {
                    _lsSietches.SelectedIndexChanged += value2;
                }
            }
        }

        internal virtual OpenFileDialog OpenFileDialog
        {
            [DebuggerNonUserCode]
            get => _OpenFileDialog;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _OpenFileDialog = value;
        }

        internal virtual TextBox txtRegion
        {
            [DebuggerNonUserCode]
            get => _txtRegion;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtRegion = value;
        }

        internal virtual TextBox txtSubRegion
        {
            [DebuggerNonUserCode]
            get => _txtSubRegion;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtSubRegion = value;
        }

        internal virtual TabControl TabControl
        {
            [DebuggerNonUserCode]
            get => _TabControl;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _TabControl = value;
        }

        internal virtual TabPage tabSietches
        {
            [DebuggerNonUserCode]
            get => _tabSietches;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _tabSietches = value;
        }

        internal virtual TabPage tabTroops
        {
            [DebuggerNonUserCode]
            get => _tabTroops;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _tabTroops = value;
        }

        internal virtual TextBox txtSpicefieldID
        {
            [DebuggerNonUserCode]
            get => _txtSpicefieldID;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtSpicefieldID = value;
        }

        internal virtual TextBox txtStatus
        {
            [DebuggerNonUserCode]
            get => _txtStatus;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = txtStatus_TextChanged;
                if (_txtStatus != null)
                {
                    _txtStatus.TextChanged -= value2;
                }
                _txtStatus = value;
                if (_txtStatus != null)
                {
                    _txtStatus.TextChanged += value2;
                }
            }
        }

        internal virtual TextBox txtHousedTroopID
        {
            [DebuggerNonUserCode]
            get => _txtHousedTroopID;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtHousedTroopID = value;
        }

        internal virtual Label Label14
        {
            [DebuggerNonUserCode]
            get => _Label14;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label14 = value;
        }

        internal virtual Label Label13
        {
            [DebuggerNonUserCode]
            get => _Label13;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label13 = value;
        }

        internal virtual Label Label12
        {
            [DebuggerNonUserCode]
            get => _Label12;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label12 = value;
        }

        internal virtual Label Label11
        {
            [DebuggerNonUserCode]
            get => _Label11;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label11 = value;
        }

        internal virtual Label Label10
        {
            [DebuggerNonUserCode]
            get => _Label10;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label10 = value;
        }

        internal virtual Label Label9
        {
            [DebuggerNonUserCode]
            get => _Label9;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label9 = value;
        }

        internal virtual Label Label8
        {
            [DebuggerNonUserCode]
            get => _Label8;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label8 = value;
        }

        internal virtual Label Label7
        {
            [DebuggerNonUserCode]
            get => _Label7;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label7 = value;
        }

        internal virtual Label Label6
        {
            [DebuggerNonUserCode]
            get => _Label6;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label6 = value;
        }

        internal virtual Label Label5
        {
            [DebuggerNonUserCode]
            get => _Label5;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label5 = value;
        }

        internal virtual Label Label4
        {
            [DebuggerNonUserCode]
            get => _Label4;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label4 = value;
        }

        internal virtual Label Label3
        {
            [DebuggerNonUserCode]
            get => _Label3;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label3 = value;
        }

        internal virtual Label Label2
        {
            [DebuggerNonUserCode]
            get => _Label2;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label2 = value;
        }

        internal virtual Label Label1
        {
            [DebuggerNonUserCode]
            get => _Label1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label1 = value;
        }

        internal virtual TabPage tabGeneral
        {
            [DebuggerNonUserCode]
            get => _tabGeneral;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _tabGeneral = value;
        }

        internal virtual Label Label15
        {
            [DebuggerNonUserCode]
            get => _Label15;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label15 = value;
        }

        internal virtual Label Label16
        {
            [DebuggerNonUserCode]
            get => _Label16;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label16 = value;
        }

        internal virtual TextBox txtEquipment
        {
            [DebuggerNonUserCode]
            get => _txtEquipment;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtEquipment = value;
        }

        internal virtual TextBox txtJob
        {
            [DebuggerNonUserCode]
            get => _txtJob;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtJob = value;
        }

        internal virtual Label Label17
        {
            [DebuggerNonUserCode]
            get => _Label17;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label17 = value;
        }

        internal virtual Label Label18
        {
            [DebuggerNonUserCode]
            get => _Label18;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label18 = value;
        }

        internal virtual Label Label19
        {
            [DebuggerNonUserCode]
            get => _Label19;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label19 = value;
        }

        internal virtual Label Label20
        {
            [DebuggerNonUserCode]
            get => _Label20;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label20 = value;
        }

        internal virtual Label Label21
        {
            [DebuggerNonUserCode]
            get => _Label21;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label21 = value;
        }

        internal virtual Label Label22
        {
            [DebuggerNonUserCode]
            get => _Label22;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label22 = value;
        }

        internal virtual Label Label23
        {
            [DebuggerNonUserCode]
            get => _Label23;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label23 = value;
        }

        internal virtual Label Label24
        {
            [DebuggerNonUserCode]
            get => _Label24;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label24 = value;
        }

        internal virtual Label Label25
        {
            [DebuggerNonUserCode]
            get => _Label25;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label25 = value;
        }

        internal virtual Label Label26
        {
            [DebuggerNonUserCode]
            get => _Label26;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label26 = value;
        }

        internal virtual ListBox lsTroops
        {
            [DebuggerNonUserCode]
            get => _lsTroops;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = lsTroops_SelectedIndexChanged;
                if (_lsTroops != null)
                {
                    _lsTroops.SelectedIndexChanged -= value2;
                }
                _lsTroops = value;
                if (_lsTroops != null)
                {
                    _lsTroops.SelectedIndexChanged += value2;
                }
            }
        }

        internal virtual TextBox txtNextTroopID
        {
            [DebuggerNonUserCode]
            get => _txtNextTroopID;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtNextTroopID = value;
        }

        internal virtual TextBox txtTroopID
        {
            [DebuggerNonUserCode]
            get => _txtTroopID;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtTroopID = value;
        }

        internal virtual Button btnSietchUpdate
        {
            [DebuggerNonUserCode]
            get => _btnSietchUpdate;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = btnSietchUpdate_Click;
                if (_btnSietchUpdate != null)
                {
                    _btnSietchUpdate.Click -= value2;
                }
                _btnSietchUpdate = value;
                if (_btnSietchUpdate != null)
                {
                    _btnSietchUpdate.Click += value2;
                }
            }
        }

        internal virtual Button btnTroopsUpdate
        {
            [DebuggerNonUserCode]
            get => _btnTroopsUpdate;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = btnTroopsUpdate_Click;
                if (_btnTroopsUpdate != null)
                {
                    _btnTroopsUpdate.Click -= value2;
                }
                _btnTroopsUpdate = value;
                if (_btnTroopsUpdate != null)
                {
                    _btnTroopsUpdate.Click += value2;
                }
            }
        }

        internal virtual ToolStrip ToolStrip1
        {
            [DebuggerNonUserCode]
            get => _ToolStrip1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _ToolStrip1 = value;
        }

        internal virtual Label Label27
        {
            [DebuggerNonUserCode]
            get => _Label27;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label27 = value;
        }

        internal virtual ComboBox cmbTroopAtSietch
        {
            [DebuggerNonUserCode]
            get => _cmbTroopAtSietch;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = cmbTroopAtSietch_SelectedIndexChanged;
                if (_cmbTroopAtSietch != null)
                {
                    _cmbTroopAtSietch.SelectedIndexChanged -= value2;
                }
                _cmbTroopAtSietch = value;
                if (_cmbTroopAtSietch != null)
                {
                    _cmbTroopAtSietch.SelectedIndexChanged += value2;
                }
            }
        }

        internal virtual Label Label28
        {
            [DebuggerNonUserCode]
            get => _Label28;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _Label28 = value;
        }

        internal virtual NumericUpDown txtSpice
        {
            [DebuggerNonUserCode]
            get => _txtSpice;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtSpice = value;
        }

        internal virtual NumericUpDown txtCharisma
        {
            [DebuggerNonUserCode]
            get => _txtCharisma;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtCharisma = value;
        }

        internal virtual NumericUpDown txtContactDistance
        {
            [DebuggerNonUserCode]
            get => _txtContactDistance;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtContactDistance = value;
        }

        internal virtual NumericUpDown txtWater
        {
            [DebuggerNonUserCode]
            get => _txtWater;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtWater != null)
                {
                    _txtWater.ValueChanged -= value2;
                }
                _txtWater = value;
                if (_txtWater != null)
                {
                    _txtWater.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtBulbs
        {
            [DebuggerNonUserCode]
            get => _txtBulbs;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtBulbs != null)
                {
                    _txtBulbs.ValueChanged -= value2;
                }
                _txtBulbs = value;
                if (_txtBulbs != null)
                {
                    _txtBulbs.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtAtomics
        {
            [DebuggerNonUserCode]
            get => _txtAtomics;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtAtomics != null)
                {
                    _txtAtomics.ValueChanged -= value2;
                }
                _txtAtomics = value;
                if (_txtAtomics != null)
                {
                    _txtAtomics.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtWeirding
        {
            [DebuggerNonUserCode]
            get => _txtWeirding;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtWeirding != null)
                {
                    _txtWeirding.ValueChanged -= value2;
                }
                _txtWeirding = value;
                if (_txtWeirding != null)
                {
                    _txtWeirding.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtLaserGuns
        {
            [DebuggerNonUserCode]
            get => _txtLaserGuns;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtLaserGuns != null)
                {
                    _txtLaserGuns.ValueChanged -= value2;
                }
                _txtLaserGuns = value;
                if (_txtLaserGuns != null)
                {
                    _txtLaserGuns.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtKrys
        {
            [DebuggerNonUserCode]
            get => _txtKrys;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtKrys != null)
                {
                    _txtKrys.ValueChanged -= value2;
                }
                _txtKrys = value;
                if (_txtKrys != null)
                {
                    _txtKrys.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtOrni
        {
            [DebuggerNonUserCode]
            get => _txtOrni;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtOrni != null)
                {
                    _txtOrni.ValueChanged -= value2;
                }
                _txtOrni = value;
                if (_txtOrni != null)
                {
                    _txtOrni.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtHarvesters
        {
            [DebuggerNonUserCode]
            get => _txtHarvesters;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtHarvesters != null)
                {
                    _txtHarvesters.ValueChanged -= value2;
                }
                _txtHarvesters = value;
                if (_txtHarvesters != null)
                {
                    _txtHarvesters.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtSpiceDensity
        {
            [DebuggerNonUserCode]
            get => _txtSpiceDensity;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchValueChanged;
                if (_txtSpiceDensity != null)
                {
                    _txtSpiceDensity.ValueChanged -= value2;
                }
                _txtSpiceDensity = value;
                if (_txtSpiceDensity != null)
                {
                    _txtSpiceDensity.ValueChanged += value2;
                }
            }
        }

        internal virtual GroupBox gbStatus
        {
            [DebuggerNonUserCode]
            get => _gbStatus;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _gbStatus = value;
        }

        internal virtual CheckBox chkSietchStatus6
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus6;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus6 != null)
                {
                    _chkSietchStatus6.CheckedChanged -= value2;
                }
                _chkSietchStatus6 = value;
                if (_chkSietchStatus6 != null)
                {
                    _chkSietchStatus6.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkSietchStatus5
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus5;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus5 != null)
                {
                    _chkSietchStatus5.CheckedChanged -= value2;
                }
                _chkSietchStatus5 = value;
                if (_chkSietchStatus5 != null)
                {
                    _chkSietchStatus5.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkSietchStatus3
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus3;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus3 != null)
                {
                    _chkSietchStatus3.CheckedChanged -= value2;
                }
                _chkSietchStatus3 = value;
                if (_chkSietchStatus3 != null)
                {
                    _chkSietchStatus3.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkSietchStatus4
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus4;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus4 != null)
                {
                    _chkSietchStatus4.CheckedChanged -= value2;
                }
                _chkSietchStatus4 = value;
                if (_chkSietchStatus4 != null)
                {
                    _chkSietchStatus4.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkSietchStatus2
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus2;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus2 != null)
                {
                    _chkSietchStatus2.CheckedChanged -= value2;
                }
                _chkSietchStatus2 = value;
                if (_chkSietchStatus2 != null)
                {
                    _chkSietchStatus2.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkSietchStatus1
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus1 != null)
                {
                    _chkSietchStatus1.CheckedChanged -= value2;
                }
                _chkSietchStatus1 = value;
                if (_chkSietchStatus1 != null)
                {
                    _chkSietchStatus1.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkSietchStatus8
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus8;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus8 != null)
                {
                    _chkSietchStatus8.CheckedChanged -= value2;
                }
                _chkSietchStatus8 = value;
                if (_chkSietchStatus8 != null)
                {
                    _chkSietchStatus8.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkSietchStatus7
        {
            [DebuggerNonUserCode]
            get => _chkSietchStatus7;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SietchStatusChange;
                if (_chkSietchStatus7 != null)
                {
                    _chkSietchStatus7.CheckedChanged -= value2;
                }
                _chkSietchStatus7 = value;
                if (_chkSietchStatus7 != null)
                {
                    _chkSietchStatus7.CheckedChanged += value2;
                }
            }
        }

        internal virtual GroupBox GroupBox1
        {
            [DebuggerNonUserCode]
            get => _GroupBox1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _GroupBox1 = value;
        }

        internal virtual TabPage tabMap
        {
            [DebuggerNonUserCode]
            get => _tabMap;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _tabMap = value;
        }

        internal virtual Label lblJobDesc
        {
            [DebuggerNonUserCode]
            get => _lblJobDesc;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _lblJobDesc = value;
        }

        internal virtual NumericUpDown txtPopulation
        {
            [DebuggerNonUserCode]
            get => _txtPopulation;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = TroopValueChanged;
                if (_txtPopulation != null)
                {
                    _txtPopulation.ValueChanged -= value2;
                }
                _txtPopulation = value;
                if (_txtPopulation != null)
                {
                    _txtPopulation.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtEcologySkill
        {
            [DebuggerNonUserCode]
            get => _txtEcologySkill;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = TroopValueChanged;
                if (_txtEcologySkill != null)
                {
                    _txtEcologySkill.ValueChanged -= value2;
                }
                _txtEcologySkill = value;
                if (_txtEcologySkill != null)
                {
                    _txtEcologySkill.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtArmySkill
        {
            [DebuggerNonUserCode]
            get => _txtArmySkill;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = TroopValueChanged;
                if (_txtArmySkill != null)
                {
                    _txtArmySkill.ValueChanged -= value2;
                }
                _txtArmySkill = value;
                if (_txtArmySkill != null)
                {
                    _txtArmySkill.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtMotivation
        {
            [DebuggerNonUserCode]
            get => _txtMotivation;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = TroopValueChanged;
                if (_txtMotivation != null)
                {
                    _txtMotivation.ValueChanged -= value2;
                }
                _txtMotivation = value;
                if (_txtMotivation != null)
                {
                    _txtMotivation.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtSpiceSkill
        {
            [DebuggerNonUserCode]
            get => _txtSpiceSkill;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = TroopValueChanged;
                if (_txtSpiceSkill != null)
                {
                    _txtSpiceSkill.ValueChanged -= value2;
                }
                _txtSpiceSkill = value;
                if (_txtSpiceSkill != null)
                {
                    _txtSpiceSkill.ValueChanged += value2;
                }
            }
        }

        internal virtual NumericUpDown txtDissatisfaction
        {
            [DebuggerNonUserCode]
            get => _txtDissatisfaction;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = TroopValueChanged;
                if (_txtDissatisfaction != null)
                {
                    _txtDissatisfaction.ValueChanged -= value2;
                }
                _txtDissatisfaction = value;
                if (_txtDissatisfaction != null)
                {
                    _txtDissatisfaction.ValueChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkHarverster
        {
            [DebuggerNonUserCode]
            get => _chkHarverster;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = EquipmentChanged;
                if (_chkHarverster != null)
                {
                    _chkHarverster.CheckedChanged -= value2;
                }
                _chkHarverster = value;
                if (_chkHarverster != null)
                {
                    _chkHarverster.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkOrni
        {
            [DebuggerNonUserCode]
            get => _chkOrni;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = EquipmentChanged;
                if (_chkOrni != null)
                {
                    _chkOrni.CheckedChanged -= value2;
                }
                _chkOrni = value;
                if (_chkOrni != null)
                {
                    _chkOrni.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkKrys
        {
            [DebuggerNonUserCode]
            get => _chkKrys;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = EquipmentChanged;
                if (_chkKrys != null)
                {
                    _chkKrys.CheckedChanged -= value2;
                }
                _chkKrys = value;
                if (_chkKrys != null)
                {
                    _chkKrys.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkLaserGuns
        {
            [DebuggerNonUserCode]
            get => _chkLaserGuns;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = EquipmentChanged;
                if (_chkLaserGuns != null)
                {
                    _chkLaserGuns.CheckedChanged -= value2;
                }
                _chkLaserGuns = value;
                if (_chkLaserGuns != null)
                {
                    _chkLaserGuns.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkWeirdings
        {
            [DebuggerNonUserCode]
            get => _chkWeirdings;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = EquipmentChanged;
                if (_chkWeirdings != null)
                {
                    _chkWeirdings.CheckedChanged -= value2;
                }
                _chkWeirdings = value;
                if (_chkWeirdings != null)
                {
                    _chkWeirdings.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkAtomics
        {
            [DebuggerNonUserCode]
            get => _chkAtomics;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = EquipmentChanged;
                if (_chkAtomics != null)
                {
                    _chkAtomics.CheckedChanged -= value2;
                }
                _chkAtomics = value;
                if (_chkAtomics != null)
                {
                    _chkAtomics.CheckedChanged += value2;
                }
            }
        }

        internal virtual CheckBox chkBulb
        {
            [DebuggerNonUserCode]
            get => _chkBulb;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = EquipmentChanged;
                if (_chkBulb != null)
                {
                    _chkBulb.CheckedChanged -= value2;
                }
                _chkBulb = value;
                if (_chkBulb != null)
                {
                    _chkBulb.CheckedChanged += value2;
                }
            }
        }

        internal virtual TextBox txtSubRegionDesc
        {
            [DebuggerNonUserCode]
            get => _txtSubRegionDesc;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtSubRegionDesc = value;
        }

        internal virtual TextBox txtRegionDesc
        {
            [DebuggerNonUserCode]
            get => _txtRegionDesc;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _txtRegionDesc = value;
        }

        internal virtual ToolStripDropDownButton ToolStripDropDownButton1
        {
            [DebuggerNonUserCode]
            get => _ToolStripDropDownButton1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _ToolStripDropDownButton1 = value;
        }

        internal virtual ToolStripMenuItem OpenToolStripMenuItem
        {
            [DebuggerNonUserCode]
            get => _OpenToolStripMenuItem;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = OpenToolStripMenuItem_Click;
                if (_OpenToolStripMenuItem != null)
                {
                    _OpenToolStripMenuItem.Click -= value2;
                }
                _OpenToolStripMenuItem = value;
                if (_OpenToolStripMenuItem != null)
                {
                    _OpenToolStripMenuItem.Click += value2;
                }
            }
        }

        internal virtual ToolStripMenuItem btnSave
        {
            [DebuggerNonUserCode]
            get => _btnSave;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = SaveToolStripMenuItem_Click;
                if (_btnSave != null)
                {
                    _btnSave.Click -= value2;
                }
                _btnSave = value;
                if (_btnSave != null)
                {
                    _btnSave.Click += value2;
                }
            }
        }

        internal virtual ToolStripSeparator ToolStripSeparator1
        {
            [DebuggerNonUserCode]
            get => _ToolStripSeparator1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _ToolStripSeparator1 = value;
        }

        internal virtual ToolStripMenuItem ExitToolStripMenuItem
        {
            [DebuggerNonUserCode]
            get => _ExitToolStripMenuItem;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set
            {
                EventHandler value2 = ExitToolStripMenuItem_Click;
                if (_ExitToolStripMenuItem != null)
                {
                    _ExitToolStripMenuItem.Click -= value2;
                }
                _ExitToolStripMenuItem = value;
                if (_ExitToolStripMenuItem != null)
                {
                    _ExitToolStripMenuItem.Click += value2;
                }
            }
        }

        internal virtual Label lblAbout
        {
            [DebuggerNonUserCode]
            get => _lblAbout;
            [MethodImpl(MethodImplOptions.Synchronized)]
            [DebuggerNonUserCode]
            set => _lblAbout = value;
        }

        public frmMain()
		{
			base.Load += frmMain_Load;
			__ENCAddToList(this);
			isLoading = true;
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			checked
			{
				lock (__ENCList)
				{
					if (__ENCList.Count == __ENCList.Capacity)
					{
						int num = 0;
						int num2 = __ENCList.Count - 1;
						int num3 = 0;
						while (true)
						{
							int num4 = num3;
							int num5 = num2;
							if (num4 > num5)
							{
								break;
							}
							WeakReference weakReference = __ENCList[num3];
							if (weakReference.IsAlive)
							{
								if (num3 != num)
								{
									__ENCList[num] = __ENCList[num3];
								}
								num++;
							}
							num3++;
						}
						__ENCList.RemoveRange(num, __ENCList.Count - num);
						__ENCList.Capacity = __ENCList.Count;
					}
					__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
				}
			}
		}

		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if ((disposing && components != null) ? true : false)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		[System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DuneEdit.frmMain));
			lsSietches = new System.Windows.Forms.ListBox();
			OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			txtRegion = new System.Windows.Forms.TextBox();
			txtSubRegion = new System.Windows.Forms.TextBox();
			TabControl = new System.Windows.Forms.TabControl();
			tabGeneral = new System.Windows.Forms.TabPage();
			lblAbout = new System.Windows.Forms.Label();
			txtCharisma = new System.Windows.Forms.NumericUpDown();
			txtContactDistance = new System.Windows.Forms.NumericUpDown();
			txtSpice = new System.Windows.Forms.NumericUpDown();
			Label27 = new System.Windows.Forms.Label();
			Label16 = new System.Windows.Forms.Label();
			Label15 = new System.Windows.Forms.Label();
			tabSietches = new System.Windows.Forms.TabPage();
			txtSubRegionDesc = new System.Windows.Forms.TextBox();
			txtRegionDesc = new System.Windows.Forms.TextBox();
			gbStatus = new System.Windows.Forms.GroupBox();
			chkSietchStatus8 = new System.Windows.Forms.CheckBox();
			chkSietchStatus7 = new System.Windows.Forms.CheckBox();
			chkSietchStatus6 = new System.Windows.Forms.CheckBox();
			chkSietchStatus5 = new System.Windows.Forms.CheckBox();
			chkSietchStatus3 = new System.Windows.Forms.CheckBox();
			chkSietchStatus4 = new System.Windows.Forms.CheckBox();
			chkSietchStatus2 = new System.Windows.Forms.CheckBox();
			chkSietchStatus1 = new System.Windows.Forms.CheckBox();
			txtWater = new System.Windows.Forms.NumericUpDown();
			txtBulbs = new System.Windows.Forms.NumericUpDown();
			txtAtomics = new System.Windows.Forms.NumericUpDown();
			txtWeirding = new System.Windows.Forms.NumericUpDown();
			txtLaserGuns = new System.Windows.Forms.NumericUpDown();
			txtKrys = new System.Windows.Forms.NumericUpDown();
			txtOrni = new System.Windows.Forms.NumericUpDown();
			txtHarvesters = new System.Windows.Forms.NumericUpDown();
			txtSpiceDensity = new System.Windows.Forms.NumericUpDown();
			btnSietchUpdate = new System.Windows.Forms.Button();
			txtSpicefieldID = new System.Windows.Forms.TextBox();
			txtStatus = new System.Windows.Forms.TextBox();
			txtHousedTroopID = new System.Windows.Forms.TextBox();
			Label14 = new System.Windows.Forms.Label();
			Label13 = new System.Windows.Forms.Label();
			Label12 = new System.Windows.Forms.Label();
			Label11 = new System.Windows.Forms.Label();
			Label10 = new System.Windows.Forms.Label();
			Label9 = new System.Windows.Forms.Label();
			Label8 = new System.Windows.Forms.Label();
			Label7 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			Label5 = new System.Windows.Forms.Label();
			Label4 = new System.Windows.Forms.Label();
			Label3 = new System.Windows.Forms.Label();
			Label2 = new System.Windows.Forms.Label();
			Label1 = new System.Windows.Forms.Label();
			tabTroops = new System.Windows.Forms.TabPage();
			txtPopulation = new System.Windows.Forms.NumericUpDown();
			txtEcologySkill = new System.Windows.Forms.NumericUpDown();
			txtArmySkill = new System.Windows.Forms.NumericUpDown();
			txtMotivation = new System.Windows.Forms.NumericUpDown();
			txtSpiceSkill = new System.Windows.Forms.NumericUpDown();
			txtDissatisfaction = new System.Windows.Forms.NumericUpDown();
			lblJobDesc = new System.Windows.Forms.Label();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			chkHarverster = new System.Windows.Forms.CheckBox();
			chkOrni = new System.Windows.Forms.CheckBox();
			chkKrys = new System.Windows.Forms.CheckBox();
			chkLaserGuns = new System.Windows.Forms.CheckBox();
			chkWeirdings = new System.Windows.Forms.CheckBox();
			chkAtomics = new System.Windows.Forms.CheckBox();
			chkBulb = new System.Windows.Forms.CheckBox();
			cmbTroopAtSietch = new System.Windows.Forms.ComboBox();
			Label28 = new System.Windows.Forms.Label();
			btnTroopsUpdate = new System.Windows.Forms.Button();
			txtEquipment = new System.Windows.Forms.TextBox();
			txtJob = new System.Windows.Forms.TextBox();
			Label17 = new System.Windows.Forms.Label();
			Label18 = new System.Windows.Forms.Label();
			Label19 = new System.Windows.Forms.Label();
			Label20 = new System.Windows.Forms.Label();
			Label21 = new System.Windows.Forms.Label();
			Label22 = new System.Windows.Forms.Label();
			Label23 = new System.Windows.Forms.Label();
			Label24 = new System.Windows.Forms.Label();
			Label25 = new System.Windows.Forms.Label();
			Label26 = new System.Windows.Forms.Label();
			lsTroops = new System.Windows.Forms.ListBox();
			txtNextTroopID = new System.Windows.Forms.TextBox();
			txtTroopID = new System.Windows.Forms.TextBox();
			tabMap = new System.Windows.Forms.TabPage();
			ToolStrip1 = new System.Windows.Forms.ToolStrip();
			ToolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			btnSave = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			TabControl.SuspendLayout();
			tabGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)txtCharisma).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtContactDistance).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtSpice).BeginInit();
			tabSietches.SuspendLayout();
			gbStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)txtWater).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtBulbs).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtAtomics).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtWeirding).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtLaserGuns).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtKrys).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtOrni).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtHarvesters).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtSpiceDensity).BeginInit();
			tabTroops.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)txtPopulation).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtEcologySkill).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtArmySkill).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtMotivation).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtSpiceSkill).BeginInit();
			((System.ComponentModel.ISupportInitialize)txtDissatisfaction).BeginInit();
			GroupBox1.SuspendLayout();
			ToolStrip1.SuspendLayout();
			SuspendLayout();
			lsSietches.FormattingEnabled = true;
			System.Drawing.Point point2 = (lsSietches.Location = new System.Drawing.Point(6, 6));
			lsSietches.Name = "lsSietches";
			System.Drawing.Size size2 = (lsSietches.Size = new System.Drawing.Size(251, 394));
			lsSietches.TabIndex = 10;
			point2 = (txtRegion.Location = new System.Drawing.Point(353, 21));
			txtRegion.Name = "txtRegion";
			txtRegion.ReadOnly = true;
			size2 = (txtRegion.Size = new System.Drawing.Size(100, 20));
			txtRegion.TabIndex = 11;
			txtRegion.Visible = false;
			point2 = (txtSubRegion.Location = new System.Drawing.Point(569, 21));
			txtSubRegion.Name = "txtSubRegion";
			txtSubRegion.ReadOnly = true;
			size2 = (txtSubRegion.Size = new System.Drawing.Size(100, 20));
			txtSubRegion.TabIndex = 12;
			txtSubRegion.Visible = false;
			TabControl.Controls.Add(tabGeneral);
			TabControl.Controls.Add(tabSietches);
			TabControl.Controls.Add(tabTroops);
			TabControl.Controls.Add(tabMap);
			TabControl.Enabled = false;
			point2 = (TabControl.Location = new System.Drawing.Point(10, 28));
			TabControl.Name = "TabControl";
			TabControl.SelectedIndex = 0;
			size2 = (TabControl.Size = new System.Drawing.Size(694, 475));
			TabControl.TabIndex = 13;
			tabGeneral.Controls.Add(lblAbout);
			tabGeneral.Controls.Add(txtCharisma);
			tabGeneral.Controls.Add(txtContactDistance);
			tabGeneral.Controls.Add(txtSpice);
			tabGeneral.Controls.Add(Label27);
			tabGeneral.Controls.Add(Label16);
			tabGeneral.Controls.Add(Label15);
			point2 = (tabGeneral.Location = new System.Drawing.Point(4, 22));
			tabGeneral.Name = "tabGeneral";
			System.Windows.Forms.Padding padding2 = (tabGeneral.Padding = new System.Windows.Forms.Padding(3));
			size2 = (tabGeneral.Size = new System.Drawing.Size(686, 449));
			tabGeneral.TabIndex = 2;
			tabGeneral.Text = "General";
			tabGeneral.UseVisualStyleBackColor = true;
			point2 = (lblAbout.Location = new System.Drawing.Point(6, 60));
			lblAbout.Name = "lblAbout";
			size2 = (lblAbout.Size = new System.Drawing.Size(674, 382));
			lblAbout.TabIndex = 9;
			lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			point2 = (txtCharisma.Location = new System.Drawing.Point(565, 22));
			decimal num2 = (txtCharisma.Maximum = new decimal(new int[4] { 127, 0, 0, 0 }));
			txtCharisma.Name = "txtCharisma";
			size2 = (txtCharisma.Size = new System.Drawing.Size(115, 20));
			txtCharisma.TabIndex = 8;
			point2 = (txtContactDistance.Location = new System.Drawing.Point(338, 22));
			num2 = (txtContactDistance.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtContactDistance.Name = "txtContactDistance";
			size2 = (txtContactDistance.Size = new System.Drawing.Size(115, 20));
			txtContactDistance.TabIndex = 7;
			num2 = (txtSpice.Increment = new decimal(new int[4] { 100, 0, 0, 0 }));
			point2 = (txtSpice.Location = new System.Drawing.Point(70, 22));
			num2 = (txtSpice.Maximum = new decimal(new int[4] { 393000, 0, 0, 0 }));
			txtSpice.Name = "txtSpice";
			size2 = (txtSpice.Size = new System.Drawing.Size(115, 20));
			txtSpice.TabIndex = 6;
			Label27.AutoSize = true;
			point2 = (Label27.Location = new System.Drawing.Point(506, 24));
			Label27.Name = "Label27";
			size2 = (Label27.Size = new System.Drawing.Size(53, 13));
			Label27.TabIndex = 4;
			Label27.Text = "Charisma:";
			Label16.AutoSize = true;
			point2 = (Label16.Location = new System.Drawing.Point(240, 24));
			Label16.Name = "Label16";
			size2 = (Label16.Size = new System.Drawing.Size(92, 13));
			Label16.TabIndex = 2;
			Label16.Text = "Contact Distance:";
			Label15.AutoSize = true;
			point2 = (Label15.Location = new System.Drawing.Point(6, 24));
			Label15.Name = "Label15";
			size2 = (Label15.Size = new System.Drawing.Size(58, 13));
			Label15.TabIndex = 0;
			Label15.Text = "Spice (kg):";
			tabSietches.Controls.Add(txtSubRegionDesc);
			tabSietches.Controls.Add(txtRegionDesc);
			tabSietches.Controls.Add(gbStatus);
			tabSietches.Controls.Add(txtWater);
			tabSietches.Controls.Add(txtBulbs);
			tabSietches.Controls.Add(txtAtomics);
			tabSietches.Controls.Add(txtWeirding);
			tabSietches.Controls.Add(txtLaserGuns);
			tabSietches.Controls.Add(txtKrys);
			tabSietches.Controls.Add(txtOrni);
			tabSietches.Controls.Add(txtHarvesters);
			tabSietches.Controls.Add(txtSpiceDensity);
			tabSietches.Controls.Add(btnSietchUpdate);
			tabSietches.Controls.Add(txtSpicefieldID);
			tabSietches.Controls.Add(txtStatus);
			tabSietches.Controls.Add(txtHousedTroopID);
			tabSietches.Controls.Add(Label14);
			tabSietches.Controls.Add(Label13);
			tabSietches.Controls.Add(Label12);
			tabSietches.Controls.Add(Label11);
			tabSietches.Controls.Add(Label10);
			tabSietches.Controls.Add(Label9);
			tabSietches.Controls.Add(Label8);
			tabSietches.Controls.Add(Label7);
			tabSietches.Controls.Add(Label6);
			tabSietches.Controls.Add(Label5);
			tabSietches.Controls.Add(Label4);
			tabSietches.Controls.Add(Label3);
			tabSietches.Controls.Add(Label2);
			tabSietches.Controls.Add(Label1);
			tabSietches.Controls.Add(lsSietches);
			tabSietches.Controls.Add(txtSubRegion);
			tabSietches.Controls.Add(txtRegion);
			point2 = (tabSietches.Location = new System.Drawing.Point(4, 22));
			tabSietches.Name = "tabSietches";
			padding2 = (tabSietches.Padding = new System.Windows.Forms.Padding(3));
			size2 = (tabSietches.Size = new System.Drawing.Size(686, 449));
			tabSietches.TabIndex = 0;
			tabSietches.Text = "Sietches";
			tabSietches.UseVisualStyleBackColor = true;
			point2 = (txtSubRegionDesc.Location = new System.Drawing.Point(569, 21));
			txtSubRegionDesc.Name = "txtSubRegionDesc";
			txtSubRegionDesc.ReadOnly = true;
			size2 = (txtSubRegionDesc.Size = new System.Drawing.Size(100, 20));
			txtSubRegionDesc.TabIndex = 59;
			point2 = (txtRegionDesc.Location = new System.Drawing.Point(353, 21));
			txtRegionDesc.Name = "txtRegionDesc";
			txtRegionDesc.ReadOnly = true;
			size2 = (txtRegionDesc.Size = new System.Drawing.Size(100, 20));
			txtRegionDesc.TabIndex = 58;
			gbStatus.Controls.Add(chkSietchStatus8);
			gbStatus.Controls.Add(chkSietchStatus7);
			gbStatus.Controls.Add(chkSietchStatus6);
			gbStatus.Controls.Add(chkSietchStatus5);
			gbStatus.Controls.Add(chkSietchStatus3);
			gbStatus.Controls.Add(chkSietchStatus4);
			gbStatus.Controls.Add(chkSietchStatus2);
			gbStatus.Controls.Add(chkSietchStatus1);
			point2 = (gbStatus.Location = new System.Drawing.Point(482, 99));
			gbStatus.Name = "gbStatus";
			size2 = (gbStatus.Size = new System.Drawing.Size(187, 238));
			gbStatus.TabIndex = 57;
			gbStatus.TabStop = false;
			gbStatus.Text = "Status";
			chkSietchStatus8.AutoSize = true;
			point2 = (chkSietchStatus8.Location = new System.Drawing.Point(13, 209));
			chkSietchStatus8.Name = "chkSietchStatus8";
			size2 = (chkSietchStatus8.Size = new System.Drawing.Size(100, 17));
			chkSietchStatus8.TabIndex = 64;
			chkSietchStatus8.Tag = "7";
			chkSietchStatus8.Text = "Not Discovered";
			chkSietchStatus8.UseVisualStyleBackColor = true;
			chkSietchStatus7.AutoSize = true;
			point2 = (chkSietchStatus7.Location = new System.Drawing.Point(13, 183));
			chkSietchStatus7.Name = "chkSietchStatus7";
			size2 = (chkSietchStatus7.Size = new System.Drawing.Size(80, 17));
			chkSietchStatus7.TabIndex = 63;
			chkSietchStatus7.Tag = "6";
			chkSietchStatus7.Text = "Prospected";
			chkSietchStatus7.UseVisualStyleBackColor = true;
			chkSietchStatus6.AutoSize = true;
			point2 = (chkSietchStatus6.Location = new System.Drawing.Point(13, 157));
			chkSietchStatus6.Name = "chkSietchStatus6";
			size2 = (chkSietchStatus6.Size = new System.Drawing.Size(76, 17));
			chkSietchStatus6.TabIndex = 62;
			chkSietchStatus6.Tag = "5";
			chkSietchStatus6.Text = "Wind-Trap";
			chkSietchStatus6.UseVisualStyleBackColor = true;
			chkSietchStatus5.AutoSize = true;
			point2 = (chkSietchStatus5.Location = new System.Drawing.Point(13, 131));
			chkSietchStatus5.Name = "chkSietchStatus5";
			size2 = (chkSietchStatus5.Size = new System.Drawing.Size(92, 17));
			chkSietchStatus5.TabIndex = 61;
			chkSietchStatus5.Tag = "4";
			chkSietchStatus5.Text = "See Inventory";
			chkSietchStatus5.UseVisualStyleBackColor = true;
			chkSietchStatus3.AutoSize = true;
			point2 = (chkSietchStatus3.Location = new System.Drawing.Point(13, 79));
			chkSietchStatus3.Name = "chkSietchStatus3";
			size2 = (chkSietchStatus3.Size = new System.Drawing.Size(94, 17));
			chkSietchStatus3.TabIndex = 60;
			chkSietchStatus3.Tag = "2";
			chkSietchStatus3.Text = "Fremen Found";
			chkSietchStatus3.UseVisualStyleBackColor = true;
			chkSietchStatus4.AutoSize = true;
			point2 = (chkSietchStatus4.Location = new System.Drawing.Point(13, 105));
			chkSietchStatus4.Name = "chkSietchStatus4";
			size2 = (chkSietchStatus4.Size = new System.Drawing.Size(79, 17));
			chkSietchStatus4.TabIndex = 59;
			chkSietchStatus4.Tag = "3";
			chkSietchStatus4.Text = "Battle Won";
			chkSietchStatus4.UseVisualStyleBackColor = true;
			chkSietchStatus2.AutoSize = true;
			point2 = (chkSietchStatus2.Location = new System.Drawing.Point(13, 53));
			chkSietchStatus2.Name = "chkSietchStatus2";
			size2 = (chkSietchStatus2.Size = new System.Drawing.Size(65, 17));
			chkSietchStatus2.TabIndex = 58;
			chkSietchStatus2.Tag = "1";
			chkSietchStatus2.Text = "In Battle";
			chkSietchStatus2.UseVisualStyleBackColor = true;
			chkSietchStatus1.AutoSize = true;
			point2 = (chkSietchStatus1.Location = new System.Drawing.Point(13, 27));
			chkSietchStatus1.Name = "chkSietchStatus1";
			size2 = (chkSietchStatus1.Size = new System.Drawing.Size(77, 17));
			chkSietchStatus1.TabIndex = 57;
			chkSietchStatus1.Tag = "0";
			chkSietchStatus1.Text = "Vegetation";
			chkSietchStatus1.UseVisualStyleBackColor = true;
			point2 = (txtWater.Location = new System.Drawing.Point(353, 315));
			num2 = (txtWater.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtWater.Name = "txtWater";
			size2 = (txtWater.Size = new System.Drawing.Size(100, 20));
			txtWater.TabIndex = 48;
			point2 = (txtBulbs.Location = new System.Drawing.Point(353, 288));
			num2 = (txtBulbs.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtBulbs.Name = "txtBulbs";
			size2 = (txtBulbs.Size = new System.Drawing.Size(100, 20));
			txtBulbs.TabIndex = 47;
			point2 = (txtAtomics.Location = new System.Drawing.Point(353, 261));
			num2 = (txtAtomics.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtAtomics.Name = "txtAtomics";
			size2 = (txtAtomics.Size = new System.Drawing.Size(100, 20));
			txtAtomics.TabIndex = 46;
			point2 = (txtWeirding.Location = new System.Drawing.Point(353, 234));
			num2 = (txtWeirding.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtWeirding.Name = "txtWeirding";
			size2 = (txtWeirding.Size = new System.Drawing.Size(100, 20));
			txtWeirding.TabIndex = 45;
			point2 = (txtLaserGuns.Location = new System.Drawing.Point(353, 207));
			num2 = (txtLaserGuns.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtLaserGuns.Name = "txtLaserGuns";
			size2 = (txtLaserGuns.Size = new System.Drawing.Size(100, 20));
			txtLaserGuns.TabIndex = 44;
			point2 = (txtKrys.Location = new System.Drawing.Point(353, 180));
			num2 = (txtKrys.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtKrys.Name = "txtKrys";
			size2 = (txtKrys.Size = new System.Drawing.Size(100, 20));
			txtKrys.TabIndex = 43;
			point2 = (txtOrni.Location = new System.Drawing.Point(353, 153));
			num2 = (txtOrni.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtOrni.Name = "txtOrni";
			size2 = (txtOrni.Size = new System.Drawing.Size(100, 20));
			txtOrni.TabIndex = 42;
			point2 = (txtHarvesters.Location = new System.Drawing.Point(353, 126));
			num2 = (txtHarvesters.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtHarvesters.Name = "txtHarvesters";
			size2 = (txtHarvesters.Size = new System.Drawing.Size(100, 20));
			txtHarvesters.TabIndex = 41;
			point2 = (txtSpiceDensity.Location = new System.Drawing.Point(353, 99));
			num2 = (txtSpiceDensity.Maximum = new decimal(new int[4] { 255, 0, 0, 0 }));
			txtSpiceDensity.Name = "txtSpiceDensity";
			size2 = (txtSpiceDensity.Size = new System.Drawing.Size(100, 20));
			txtSpiceDensity.TabIndex = 40;
			btnSietchUpdate.Enabled = false;
			point2 = (btnSietchUpdate.Location = new System.Drawing.Point(605, 420));
			btnSietchUpdate.Name = "btnSietchUpdate";
			size2 = (btnSietchUpdate.Size = new System.Drawing.Size(75, 23));
			btnSietchUpdate.TabIndex = 39;
			btnSietchUpdate.Text = "Update";
			btnSietchUpdate.UseVisualStyleBackColor = true;
			point2 = (txtSpicefieldID.Location = new System.Drawing.Point(569, 50));
			txtSpicefieldID.Name = "txtSpicefieldID";
			txtSpicefieldID.ReadOnly = true;
			size2 = (txtSpicefieldID.Size = new System.Drawing.Size(100, 20));
			txtSpicefieldID.TabIndex = 29;
			point2 = (txtStatus.Location = new System.Drawing.Point(353, 73));
			txtStatus.Name = "txtStatus";
			txtStatus.ReadOnly = true;
			size2 = (txtStatus.Size = new System.Drawing.Size(100, 20));
			txtStatus.TabIndex = 28;
			point2 = (txtHousedTroopID.Location = new System.Drawing.Point(353, 47));
			txtHousedTroopID.Name = "txtHousedTroopID";
			txtHousedTroopID.ReadOnly = true;
			size2 = (txtHousedTroopID.Size = new System.Drawing.Size(100, 20));
			txtHousedTroopID.TabIndex = 27;
			Label14.AutoSize = true;
			point2 = (Label14.Location = new System.Drawing.Point(263, 317));
			Label14.Name = "Label14";
			size2 = (Label14.Size = new System.Drawing.Size(39, 13));
			Label14.TabIndex = 26;
			Label14.Text = "Water:";
			Label13.AutoSize = true;
			point2 = (Label13.Location = new System.Drawing.Point(263, 290));
			Label13.Name = "Label13";
			size2 = (Label13.Size = new System.Drawing.Size(36, 13));
			Label13.TabIndex = 25;
			Label13.Text = "Bulbs:";
			Label12.AutoSize = true;
			point2 = (Label12.Location = new System.Drawing.Point(263, 263));
			Label12.Name = "Label12";
			size2 = (Label12.Size = new System.Drawing.Size(47, 13));
			Label12.TabIndex = 24;
			Label12.Text = "Atomics:";
			Label11.AutoSize = true;
			point2 = (Label11.Location = new System.Drawing.Point(263, 236));
			Label11.Name = "Label11";
			size2 = (Label11.Size = new System.Drawing.Size(76, 13));
			Label11.TabIndex = 23;
			Label11.Text = "Weirding Mod:";
			Label10.AutoSize = true;
			point2 = (Label10.Location = new System.Drawing.Point(263, 209));
			Label10.Name = "Label10";
			size2 = (Label10.Size = new System.Drawing.Size(64, 13));
			Label10.TabIndex = 22;
			Label10.Text = "Laser Guns:";
			Label9.AutoSize = true;
			point2 = (Label9.Location = new System.Drawing.Point(263, 182));
			Label9.Name = "Label9";
			size2 = (Label9.Size = new System.Drawing.Size(64, 13));
			Label9.TabIndex = 21;
			Label9.Text = "Krys/Knifes:";
			Label8.AutoSize = true;
			point2 = (Label8.Location = new System.Drawing.Point(263, 155));
			Label8.Name = "Label8";
			size2 = (Label8.Size = new System.Drawing.Size(67, 13));
			Label8.TabIndex = 20;
			Label8.Text = "Ornithopters:";
			Label7.AutoSize = true;
			point2 = (Label7.Location = new System.Drawing.Point(263, 128));
			Label7.Name = "Label7";
			size2 = (Label7.Size = new System.Drawing.Size(61, 13));
			Label7.TabIndex = 19;
			Label7.Text = "Harvesters:";
			Label6.AutoSize = true;
			point2 = (Label6.Location = new System.Drawing.Point(263, 101));
			Label6.Name = "Label6";
			size2 = (Label6.Size = new System.Drawing.Size(75, 13));
			Label6.TabIndex = 18;
			Label6.Text = "Spice Density:";
			Label5.AutoSize = true;
			point2 = (Label5.Location = new System.Drawing.Point(479, 53));
			Label5.Name = "Label5";
			size2 = (Label5.Size = new System.Drawing.Size(70, 13));
			Label5.TabIndex = 17;
			Label5.Text = "Spicefield ID:";
			Label4.AutoSize = true;
			point2 = (Label4.Location = new System.Drawing.Point(263, 76));
			Label4.Name = "Label4";
			size2 = (Label4.Size = new System.Drawing.Size(40, 13));
			Label4.TabIndex = 16;
			Label4.Text = "Status:";
			Label3.AutoSize = true;
			point2 = (Label3.Location = new System.Drawing.Point(263, 50));
			Label3.Name = "Label3";
			size2 = (Label3.Size = new System.Drawing.Size(78, 13));
			Label3.TabIndex = 15;
			Label3.Text = "Housed Troop:";
			Label2.AutoSize = true;
			point2 = (Label2.Location = new System.Drawing.Point(479, 24));
			Label2.Name = "Label2";
			size2 = (Label2.Size = new System.Drawing.Size(66, 13));
			Label2.TabIndex = 14;
			Label2.Text = "Sub Region:";
			Label1.AutoSize = true;
			point2 = (Label1.Location = new System.Drawing.Point(263, 24));
			Label1.Name = "Label1";
			size2 = (Label1.Size = new System.Drawing.Size(44, 13));
			Label1.TabIndex = 13;
			Label1.Text = "Region:";
			tabTroops.Controls.Add(txtPopulation);
			tabTroops.Controls.Add(txtEcologySkill);
			tabTroops.Controls.Add(txtArmySkill);
			tabTroops.Controls.Add(txtMotivation);
			tabTroops.Controls.Add(txtSpiceSkill);
			tabTroops.Controls.Add(txtDissatisfaction);
			tabTroops.Controls.Add(lblJobDesc);
			tabTroops.Controls.Add(GroupBox1);
			tabTroops.Controls.Add(cmbTroopAtSietch);
			tabTroops.Controls.Add(Label28);
			tabTroops.Controls.Add(btnTroopsUpdate);
			tabTroops.Controls.Add(txtEquipment);
			tabTroops.Controls.Add(txtJob);
			tabTroops.Controls.Add(Label17);
			tabTroops.Controls.Add(Label18);
			tabTroops.Controls.Add(Label19);
			tabTroops.Controls.Add(Label20);
			tabTroops.Controls.Add(Label21);
			tabTroops.Controls.Add(Label22);
			tabTroops.Controls.Add(Label23);
			tabTroops.Controls.Add(Label24);
			tabTroops.Controls.Add(Label25);
			tabTroops.Controls.Add(Label26);
			tabTroops.Controls.Add(lsTroops);
			tabTroops.Controls.Add(txtNextTroopID);
			tabTroops.Controls.Add(txtTroopID);
			point2 = (tabTroops.Location = new System.Drawing.Point(4, 22));
			tabTroops.Name = "tabTroops";
			padding2 = (tabTroops.Padding = new System.Windows.Forms.Padding(3));
			size2 = (tabTroops.Size = new System.Drawing.Size(686, 449));
			tabTroops.TabIndex = 1;
			tabTroops.Text = "Troops";
			tabTroops.UseVisualStyleBackColor = true;
			point2 = (txtPopulation.Location = new System.Drawing.Point(353, 322));
			num2 = (txtPopulation.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 }));
			txtPopulation.Name = "txtPopulation";
			size2 = (txtPopulation.Size = new System.Drawing.Size(100, 20));
			txtPopulation.TabIndex = 66;
			point2 = (txtEcologySkill.Location = new System.Drawing.Point(353, 280));
			num2 = (txtEcologySkill.Maximum = new decimal(new int[4] { 95, 0, 0, 0 }));
			txtEcologySkill.Name = "txtEcologySkill";
			size2 = (txtEcologySkill.Size = new System.Drawing.Size(100, 20));
			txtEcologySkill.TabIndex = 65;
			point2 = (txtArmySkill.Location = new System.Drawing.Point(353, 238));
			num2 = (txtArmySkill.Maximum = new decimal(new int[4] { 95, 0, 0, 0 }));
			txtArmySkill.Name = "txtArmySkill";
			size2 = (txtArmySkill.Size = new System.Drawing.Size(100, 20));
			txtArmySkill.TabIndex = 64;
			point2 = (txtMotivation.Location = new System.Drawing.Point(353, 154));
			txtMotivation.Name = "txtMotivation";
			size2 = (txtMotivation.Size = new System.Drawing.Size(100, 20));
			txtMotivation.TabIndex = 63;
			point2 = (txtSpiceSkill.Location = new System.Drawing.Point(353, 196));
			num2 = (txtSpiceSkill.Maximum = new decimal(new int[4] { 95, 0, 0, 0 }));
			txtSpiceSkill.Name = "txtSpiceSkill";
			size2 = (txtSpiceSkill.Size = new System.Drawing.Size(100, 20));
			txtSpiceSkill.TabIndex = 63;
			point2 = (txtDissatisfaction.Location = new System.Drawing.Point(353, 112));
			txtDissatisfaction.Name = "txtDissatisfaction";
			size2 = (txtDissatisfaction.Size = new System.Drawing.Size(100, 20));
			txtDissatisfaction.TabIndex = 62;
			lblJobDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			point2 = (lblJobDesc.Location = new System.Drawing.Point(480, 79));
			lblJobDesc.Name = "lblJobDesc";
			size2 = (lblJobDesc.Size = new System.Drawing.Size(180, 13));
			lblJobDesc.TabIndex = 61;
			GroupBox1.Controls.Add(chkHarverster);
			GroupBox1.Controls.Add(chkOrni);
			GroupBox1.Controls.Add(chkKrys);
			GroupBox1.Controls.Add(chkLaserGuns);
			GroupBox1.Controls.Add(chkWeirdings);
			GroupBox1.Controls.Add(chkAtomics);
			GroupBox1.Controls.Add(chkBulb);
			point2 = (GroupBox1.Location = new System.Drawing.Point(480, 112));
			GroupBox1.Name = "GroupBox1";
			size2 = (GroupBox1.Size = new System.Drawing.Size(200, 233));
			GroupBox1.TabIndex = 60;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Equipment:";
			chkHarverster.AutoSize = true;
			point2 = (chkHarverster.Location = new System.Drawing.Point(15, 202));
			chkHarverster.Name = "chkHarverster";
			size2 = (chkHarverster.Size = new System.Drawing.Size(107, 17));
			chkHarverster.TabIndex = 6;
			chkHarverster.Tag = "7";
			chkHarverster.Text = "Spice Harvesters";
			chkHarverster.UseVisualStyleBackColor = true;
			chkOrni.AutoSize = true;
			point2 = (chkOrni.Location = new System.Drawing.Point(15, 172));
			chkOrni.Name = "chkOrni";
			size2 = (chkOrni.Size = new System.Drawing.Size(83, 17));
			chkOrni.TabIndex = 5;
			chkOrni.Tag = "6";
			chkOrni.Text = "Ornithopters";
			chkOrni.UseVisualStyleBackColor = true;
			chkKrys.AutoSize = true;
			point2 = (chkKrys.Location = new System.Drawing.Point(15, 142));
			chkKrys.Name = "chkKrys";
			size2 = (chkKrys.Size = new System.Drawing.Size(81, 17));
			chkKrys.TabIndex = 4;
			chkKrys.Tag = "5";
			chkKrys.Text = "Krys Knives";
			chkKrys.UseVisualStyleBackColor = true;
			chkLaserGuns.AutoSize = true;
			point2 = (chkLaserGuns.Location = new System.Drawing.Point(15, 112));
			chkLaserGuns.Name = "chkLaserGuns";
			size2 = (chkLaserGuns.Size = new System.Drawing.Size(80, 17));
			chkLaserGuns.TabIndex = 3;
			chkLaserGuns.Tag = "4";
			chkLaserGuns.Text = "Laser Guns";
			chkLaserGuns.UseVisualStyleBackColor = true;
			chkWeirdings.AutoSize = true;
			point2 = (chkWeirdings.Location = new System.Drawing.Point(15, 82));
			chkWeirdings.Name = "chkWeirdings";
			size2 = (chkWeirdings.Size = new System.Drawing.Size(111, 17));
			chkWeirdings.TabIndex = 2;
			chkWeirdings.Tag = "3";
			chkWeirdings.Text = "Weirding Modules";
			chkWeirdings.UseVisualStyleBackColor = true;
			chkAtomics.AutoSize = true;
			point2 = (chkAtomics.Location = new System.Drawing.Point(15, 52));
			chkAtomics.Name = "chkAtomics";
			size2 = (chkAtomics.Size = new System.Drawing.Size(107, 17));
			chkAtomics.TabIndex = 1;
			chkAtomics.Tag = "2";
			chkAtomics.Text = "Atomic Weapons";
			chkAtomics.UseVisualStyleBackColor = true;
			chkBulb.AutoSize = true;
			point2 = (chkBulb.Location = new System.Drawing.Point(15, 22));
			chkBulb.Name = "chkBulb";
			size2 = (chkBulb.Size = new System.Drawing.Size(52, 17));
			chkBulb.TabIndex = 0;
			chkBulb.Tag = "1";
			chkBulb.Text = "Bulbs";
			chkBulb.UseVisualStyleBackColor = true;
			cmbTroopAtSietch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cmbTroopAtSietch.FormattingEnabled = true;
			point2 = (cmbTroopAtSietch.Location = new System.Drawing.Point(95, 10));
			cmbTroopAtSietch.Name = "cmbTroopAtSietch";
			size2 = (cmbTroopAtSietch.Size = new System.Drawing.Size(162, 21));
			cmbTroopAtSietch.TabIndex = 59;
			Label28.AutoSize = true;
			point2 = (Label28.Location = new System.Drawing.Point(6, 13));
			Label28.Name = "Label28";
			size2 = (Label28.Size = new System.Drawing.Size(83, 13));
			Label28.TabIndex = 58;
			Label28.Text = "Troop at Sietch:";
			btnTroopsUpdate.Enabled = false;
			point2 = (btnTroopsUpdate.Location = new System.Drawing.Point(605, 420));
			btnTroopsUpdate.Name = "btnTroopsUpdate";
			size2 = (btnTroopsUpdate.Size = new System.Drawing.Size(75, 23));
			btnTroopsUpdate.TabIndex = 56;
			btnTroopsUpdate.Text = "Update";
			btnTroopsUpdate.UseVisualStyleBackColor = true;
			point2 = (txtEquipment.Location = new System.Drawing.Point(353, 364));
			txtEquipment.Name = "txtEquipment";
			txtEquipment.ReadOnly = true;
			size2 = (txtEquipment.Size = new System.Drawing.Size(100, 20));
			txtEquipment.TabIndex = 54;
			txtEquipment.Visible = false;
			point2 = (txtJob.Location = new System.Drawing.Point(353, 76));
			txtJob.Name = "txtJob";
			txtJob.ReadOnly = true;
			size2 = (txtJob.Size = new System.Drawing.Size(100, 20));
			txtJob.TabIndex = 48;
			Label17.AutoSize = true;
			point2 = (Label17.Location = new System.Drawing.Point(263, 324));
			Label17.Name = "Label17";
			size2 = (Label17.Size = new System.Drawing.Size(60, 13));
			Label17.TabIndex = 47;
			Label17.Text = "Population:";
			Label18.AutoSize = true;
			point2 = (Label18.Location = new System.Drawing.Point(263, 367));
			Label18.Name = "Label18";
			size2 = (Label18.Size = new System.Drawing.Size(60, 13));
			Label18.TabIndex = 46;
			Label18.Text = "Equipment:";
			Label18.Visible = false;
			Label19.AutoSize = true;
			point2 = (Label19.Location = new System.Drawing.Point(263, 282));
			Label19.Name = "Label19";
			size2 = (Label19.Size = new System.Drawing.Size(70, 13));
			Label19.TabIndex = 45;
			Label19.Text = "Ecology Skill:";
			Label20.AutoSize = true;
			point2 = (Label20.Location = new System.Drawing.Point(263, 240));
			Label20.Name = "Label20";
			size2 = (Label20.Size = new System.Drawing.Size(55, 13));
			Label20.TabIndex = 44;
			Label20.Text = "Army Skill:";
			Label21.AutoSize = true;
			point2 = (Label21.Location = new System.Drawing.Point(263, 198));
			Label21.Name = "Label21";
			size2 = (Label21.Size = new System.Drawing.Size(59, 13));
			Label21.TabIndex = 43;
			Label21.Text = "Spice Skill:";
			Label22.AutoSize = true;
			point2 = (Label22.Location = new System.Drawing.Point(263, 156));
			Label22.Name = "Label22";
			size2 = (Label22.Size = new System.Drawing.Size(59, 13));
			Label22.TabIndex = 42;
			Label22.Text = "Motivation:";
			Label23.AutoSize = true;
			point2 = (Label23.Location = new System.Drawing.Point(263, 114));
			Label23.Name = "Label23";
			size2 = (Label23.Size = new System.Drawing.Size(78, 13));
			Label23.TabIndex = 41;
			Label23.Text = "Dissatisfaction:";
			Label24.AutoSize = true;
			point2 = (Label24.Location = new System.Drawing.Point(263, 79));
			Label24.Name = "Label24";
			size2 = (Label24.Size = new System.Drawing.Size(27, 13));
			Label24.TabIndex = 40;
			Label24.Text = "Job:";
			Label25.AutoSize = true;
			point2 = (Label25.Location = new System.Drawing.Point(477, 48));
			Label25.Name = "Label25";
			size2 = (Label25.Size = new System.Drawing.Size(77, 13));
			Label25.TabIndex = 39;
			Label25.Text = "Next Troop ID:";
			Label26.AutoSize = true;
			point2 = (Label26.Location = new System.Drawing.Point(263, 48));
			Label26.Name = "Label26";
			size2 = (Label26.Size = new System.Drawing.Size(52, 13));
			Label26.TabIndex = 38;
			Label26.Text = "Troop ID:";
			lsTroops.FormattingEnabled = true;
			point2 = (lsTroops.Location = new System.Drawing.Point(6, 45));
			lsTroops.Name = "lsTroops";
			size2 = (lsTroops.Size = new System.Drawing.Size(251, 355));
			lsTroops.TabIndex = 35;
			point2 = (txtNextTroopID.Location = new System.Drawing.Point(560, 45));
			txtNextTroopID.Name = "txtNextTroopID";
			txtNextTroopID.ReadOnly = true;
			size2 = (txtNextTroopID.Size = new System.Drawing.Size(100, 20));
			txtNextTroopID.TabIndex = 37;
			point2 = (txtTroopID.Location = new System.Drawing.Point(353, 45));
			txtTroopID.Name = "txtTroopID";
			txtTroopID.ReadOnly = true;
			size2 = (txtTroopID.Size = new System.Drawing.Size(100, 20));
			txtTroopID.TabIndex = 36;
			tabMap.BackgroundImage = DuneEdit.My.Resources.Resources.Map1;
			tabMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			point2 = (tabMap.Location = new System.Drawing.Point(4, 22));
			tabMap.Name = "tabMap";
			padding2 = (tabMap.Padding = new System.Windows.Forms.Padding(3));
			size2 = (tabMap.Size = new System.Drawing.Size(686, 449));
			tabMap.TabIndex = 4;
			tabMap.Text = "Map of Dune";
			tabMap.UseVisualStyleBackColor = true;
			ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { ToolStripDropDownButton1 });
			point2 = (ToolStrip1.Location = new System.Drawing.Point(0, 0));
			ToolStrip1.Name = "ToolStrip1";
			size2 = (ToolStrip1.Size = new System.Drawing.Size(716, 25));
			ToolStrip1.TabIndex = 14;
			ToolStrip1.Text = "ToolStrip1";
			ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			ToolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { OpenToolStripMenuItem, btnSave, ToolStripSeparator1, ExitToolStripMenuItem });
			ToolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("ToolStripDropDownButton1.Image");
			ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			ToolStripDropDownButton1.Name = "ToolStripDropDownButton1";
			size2 = (ToolStripDropDownButton1.Size = new System.Drawing.Size(38, 22));
			ToolStripDropDownButton1.Text = "File";
			OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
			size2 = (OpenToolStripMenuItem.Size = new System.Drawing.Size(103, 22));
			OpenToolStripMenuItem.Text = "Open";
			btnSave.Enabled = false;
			btnSave.Name = "btnSave";
			size2 = (btnSave.Size = new System.Drawing.Size(103, 22));
			btnSave.Text = "Save";
			ToolStripSeparator1.Name = "ToolStripSeparator1";
			size2 = (ToolStripSeparator1.Size = new System.Drawing.Size(100, 6));
			ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			size2 = (ExitToolStripMenuItem.Size = new System.Drawing.Size(103, 22));
			ExitToolStripMenuItem.Text = "Exit";
			System.Drawing.SizeF sizeF2 = (AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f));
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			size2 = (ClientSize = new System.Drawing.Size(716, 516));
			Controls.Add(ToolStrip1);
			Controls.Add(TabControl);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			Name = "frmMain";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Dune Savegame Editor (Version: 1.1)";
			TabControl.ResumeLayout(false);
			tabGeneral.ResumeLayout(false);
			tabGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)txtCharisma).EndInit();
			((System.ComponentModel.ISupportInitialize)txtContactDistance).EndInit();
			((System.ComponentModel.ISupportInitialize)txtSpice).EndInit();
			tabSietches.ResumeLayout(false);
			tabSietches.PerformLayout();
			gbStatus.ResumeLayout(false);
			gbStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)txtWater).EndInit();
			((System.ComponentModel.ISupportInitialize)txtBulbs).EndInit();
			((System.ComponentModel.ISupportInitialize)txtAtomics).EndInit();
			((System.ComponentModel.ISupportInitialize)txtWeirding).EndInit();
			((System.ComponentModel.ISupportInitialize)txtLaserGuns).EndInit();
			((System.ComponentModel.ISupportInitialize)txtKrys).EndInit();
			((System.ComponentModel.ISupportInitialize)txtOrni).EndInit();
			((System.ComponentModel.ISupportInitialize)txtHarvesters).EndInit();
			((System.ComponentModel.ISupportInitialize)txtSpiceDensity).EndInit();
			tabTroops.ResumeLayout(false);
			tabTroops.PerformLayout();
			((System.ComponentModel.ISupportInitialize)txtPopulation).EndInit();
			((System.ComponentModel.ISupportInitialize)txtEcologySkill).EndInit();
			((System.ComponentModel.ISupportInitialize)txtArmySkill).EndInit();
			((System.ComponentModel.ISupportInitialize)txtMotivation).EndInit();
			((System.ComponentModel.ISupportInitialize)txtSpiceSkill).EndInit();
			((System.ComponentModel.ISupportInitialize)txtDissatisfaction).EndInit();
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			ToolStrip1.ResumeLayout(false);
			ToolStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		private void PopulateTroops(List<byte> sg)
		{
			Troops = new Troops_List(sg);
			lsTroops.DisplayMember = "troopDesc";
			lsTroops.ValueMember = "troopID";
			lsTroops.DataSource = Troops.ToList();
			PopulateEquipment();
		}

		private void PopulateGenerals(List<byte> sg)
		{
			General = new Generals(ref sg);
			txtSpice.Text = Conversions.ToString(General.Spice);
			txtContactDistance.Text = Conversions.ToString(General.contactDistance);
			txtCharisma.Text = Conversions.ToString(General.CharismaGUI);
		}

		private void PopulateSietchList(List<byte> sg)
		{
			Sietches = new Sietch_List(ref sg);
			lsSietches.DisplayMember = "RegionName";
			lsSietches.ValueMember = "ID";
			lsSietches.DataSource = Sietches.ToList();
			cmbTroopAtSietch.DisplayMember = "RegionName";
			cmbTroopAtSietch.ValueMember = "ID";
			cmbTroopAtSietch.DataSource = Sietches.ToList();
			PopulateStatuses();
		}

		private void lsSietches_SelectedIndexChanged(object sender, EventArgs e)
		{
			isLoading = true;
			ClearInput_Sietched();
			if (lsSietches.SelectedIndex > -1)
			{
				Sietch_Item sietch_Item = (Sietch_Item)lsSietches.SelectedItem;
				string[] array = Strings.Split(sietch_Item.ID, ",");
				IEnumerable<Sietch_Item> source = Sietches.Where((Sietch_Item DS) => ((double)(int)DS.region == Conversions.ToDouble(array[0])) & ((double)(int)DS.subRegion == Conversions.ToDouble(array[1])));
				if (source.Count() > 0)
				{
					Sietch_Item sietch_Item2 = source.ElementAtOrDefault(0);
					txtRegion.Text = Conversions.ToString(sietch_Item2.region);
					txtSubRegion.Text = Conversions.ToString(sietch_Item2.subRegion);
					txtHousedTroopID.Text = Conversions.ToString(sietch_Item2.housedTroopID);
					txtStatus.Text = Conversions.ToString(sietch_Item2.Status);
					txtSpicefieldID.Text = Conversions.ToString(sietch_Item2.SpicefieldID);
					txtSpiceDensity.Text = Conversions.ToString(sietch_Item2.SpiceDensity);
					txtHarvesters.Text = Conversions.ToString(sietch_Item2.Harvesters);
					txtOrni.Text = Conversions.ToString(sietch_Item2.Orni);
					txtKrys.Text = Conversions.ToString(sietch_Item2.Krys);
					txtLaserGuns.Text = Conversions.ToString(sietch_Item2.laserGuns);
					txtWeirding.Text = Conversions.ToString(sietch_Item2.weirdingMod);
					txtAtomics.Text = Conversions.ToString(sietch_Item2.atomics);
					txtBulbs.Text = Conversions.ToString(sietch_Item2.Bulbs);
					txtWater.Text = Conversions.ToString(sietch_Item2.Water);
					txtRegionDesc.Text = Regions.region(sietch_Item2.region);
					txtSubRegionDesc.Text = Regions.subregion(sietch_Item2.subRegion);
					sietch_Item2 = null;
				}
				PopulateStatuses();
			}
			isLoading = false;
		}

		private void ClearInput_Sietched()
		{
			txtRegion.Text = string.Empty;
			txtSubRegion.Text = string.Empty;
			txtHousedTroopID.Text = string.Empty;
			txtStatus.Text = string.Empty;
			txtSpicefieldID.Text = string.Empty;
			txtSpiceDensity.Text = string.Empty;
			txtHarvesters.Text = string.Empty;
			txtOrni.Text = string.Empty;
			txtKrys.Text = string.Empty;
			txtLaserGuns.Text = string.Empty;
			txtWeirding.Text = string.Empty;
			txtAtomics.Text = string.Empty;
			txtBulbs.Text = string.Empty;
			txtWater.Text = string.Empty;
			chkSietchStatus1.Checked = false;
			chkSietchStatus2.Checked = false;
			chkSietchStatus3.Checked = false;
			chkSietchStatus4.Checked = false;
			chkSietchStatus5.Checked = false;
			chkSietchStatus6.Checked = false;
			btnSietchUpdate.Enabled = false;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			lblAbout.Text = Resources.About;
		}

		private void lsTroops_SelectedIndexChanged(object sender, EventArgs e)
		{
			isLoading = true;
			ClearInput_Troops();
			if (lsTroops.SelectedIndex > -1)
			{
				Troops_Item troops_Item = (Troops_Item)lsTroops.SelectedItem;
				string value = Conversions.ToString(troops_Item.troopID);
				IEnumerable<Troops_Item> source = Troops.Where((Troops_Item DS) => (double)(int)DS.troopID == Conversions.ToDouble(value));
				if (source.Count() > 0)
				{
					Troops_Item troops_Item2 = source.ElementAtOrDefault(0);
					txtTroopID.Text = Conversions.ToString(troops_Item2.troopID);
					txtNextTroopID.Text = Conversions.ToString(troops_Item2.nextTroopInSietch);
					txtJob.Text = Conversions.ToString(troops_Item2.job);
					txtDissatisfaction.Text = Conversions.ToString(troops_Item2.Dissatisfaction);
					txtMotivation.Text = Conversions.ToString(troops_Item2.Motivation);
					txtSpiceSkill.Text = Conversions.ToString(troops_Item2.SpiceSkill);
					txtArmySkill.Text = Conversions.ToString(troops_Item2.ArmySkill);
					txtEcologySkill.Text = Conversions.ToString(troops_Item2.EcologySkill);
					txtEquipment.Text = Conversions.ToString(troops_Item2.Equipment);
					txtPopulation.Text = Conversions.ToString(troops_Item2.Population);
					lblJobDesc.Text = Jobs.job(troops_Item2.job);
					troops_Item2 = null;
				}
				PopulateEquipment();
			}
			isLoading = false;
		}

		private void ClearInput_Troops()
		{
			txtTroopID.Text = string.Empty;
			txtNextTroopID.Text = string.Empty;
			txtJob.Text = string.Empty;
			txtDissatisfaction.Text = string.Empty;
			txtMotivation.Text = string.Empty;
			txtSpiceSkill.Text = string.Empty;
			txtArmySkill.Text = string.Empty;
			txtEcologySkill.Text = string.Empty;
			txtEquipment.Text = string.Empty;
			txtPopulation.Text = string.Empty;
			lblJobDesc.Text = string.Empty;
			chkBulb.Checked = false;
			chkAtomics.Checked = false;
			chkWeirdings.Checked = false;
			chkLaserGuns.Checked = false;
			chkKrys.Checked = false;
			chkOrni.Checked = false;
			chkHarverster.Checked = false;
			btnTroopsUpdate.Enabled = false;
		}

		private void txtStatus_TextChanged(object sender, EventArgs e)
		{
			if (!isLoading)
			{
				string[] array = lsSietches.SelectedValue.ToString().Split(',');
			}
		}

		private void btnSietchUpdate_Click(object sender, EventArgs e)
		{
			Sietches.Update(Conversions.ToByte(txtRegion.Text), Conversions.ToByte(txtSubRegion.Text), Conversions.ToByte(txtStatus.Text), Conversions.ToByte(txtSpiceDensity.Text), Conversions.ToByte(txtHarvesters.Text), Conversions.ToByte(txtOrni.Text), Conversions.ToByte(txtKrys.Text), Conversions.ToByte(txtLaserGuns.Text), Conversions.ToByte(txtWeirding.Text), Conversions.ToByte(txtAtomics.Text), Conversions.ToByte(txtBulbs.Text), Conversions.ToByte(txtWater.Text));
			btnSietchUpdate.Enabled = false;
		}

		private void btnTroopsUpdate_Click(object sender, EventArgs e)
		{
			Troops.Update(Conversions.ToInteger(lsTroops.SelectedValue), Conversions.ToByte(txtJob.Text), Conversions.ToByte(txtDissatisfaction.Text), Conversions.ToByte(txtMotivation.Text), Conversions.ToByte(txtSpiceSkill.Text), Conversions.ToByte(txtArmySkill.Text), Conversions.ToByte(txtEcologySkill.Text), Conversions.ToByte(txtEquipment.Text), Conversions.ToInteger(txtPopulation.Text));
			btnTroopsUpdate.Enabled = false;
		}

		private void cmbTroopAtSietch_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbTroopAtSietch.SelectedIndex == -1)
			{
				return;
			}
			isLoading = true;
			Sietch_Item sietch_Item = (Sietch_Item)cmbTroopAtSietch.SelectedItem;
			lsTroops.SelectedIndex = -1;
			IEnumerator enumerator = default(IEnumerator);
			try
			{
				enumerator = lsTroops.Items.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Troops_Item troops_Item = (Troops_Item)enumerator.Current;
					if (Operators.CompareString(troops_Item.Coordinates, sietch_Item.Coordinates, TextCompare: false) == 0)
					{
						lsTroops.SelectedValue = troops_Item.troopID;
						break;
					}
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			isLoading = false;
		}

		private void PopulateStatuses()
		{
			Sietch_Item sietch_Item = (Sietch_Item)lsSietches.SelectedItem;
			chkSietchStatus1.Checked = sietch_Item.hasVegetation;
			chkSietchStatus2.Checked = sietch_Item.inBattle;
			chkSietchStatus3.Checked = sietch_Item.fremenFound;
			chkSietchStatus4.Checked = sietch_Item.battleWon;
			chkSietchStatus5.Checked = sietch_Item.SeeInventory;
			chkSietchStatus6.Checked = sietch_Item.hasWindtrap;
			chkSietchStatus7.Checked = sietch_Item.prospected;
			chkSietchStatus8.Checked = sietch_Item.notDiscovered;
		}

		private void PopulateEquipment()
		{
			Troops_Item troops_Item = (Troops_Item)lsTroops.SelectedItem;
			chkBulb.Checked = troops_Item.Bulbs;
			chkAtomics.Checked = troops_Item.Atomics;
			chkWeirdings.Checked = troops_Item.Weirdings;
			chkLaserGuns.Checked = troops_Item.LaserGuns;
			chkKrys.Checked = troops_Item.KrysKnives;
			chkOrni.Checked = troops_Item.Ornithopters;
			chkHarverster.Checked = troops_Item.Harvesters;
		}

		private void SietchValueChanged(object sender, EventArgs e)
		{
			if (!isLoading)
			{
				btnSietchUpdate.Enabled = true;
			}
		}

		private void SietchStatusChange(object sender, EventArgs e)
		{
			if (!isLoading)
			{
				btnSietchUpdate.Enabled = true;
				byte b = Conversions.ToByte(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null));
				Sietch_Item sietch_Item = (Sietch_Item)lsSietches.SelectedItem;
				clsBitfield bitField = sietch_Item.BitField;
				bitField.setBit(b, ((CheckBox)sender).Checked);
				txtStatus.Text = Conversions.ToString(bitField.bitfield);
			}
		}

		private void TroopValueChanged(object sender, EventArgs e)
		{
			if (!isLoading)
			{
				btnTroopsUpdate.Enabled = true;
			}
		}

		private void EquipmentChanged(object sender, EventArgs e)
		{
			if (!isLoading)
			{
				btnTroopsUpdate.Enabled = true;
				byte b = Conversions.ToByte(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null));
				clsBitfield clsBitfield2 = new clsBitfield(Conversions.ToInteger(txtEquipment.Text));
				clsBitfield2.setBit(b, ((CheckBox)sender).Checked);
				txtEquipment.Text = Conversions.ToString(clsBitfield2.bitfield);
			}
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dispose();
			Close();
		}

		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.FileName = string.Empty;
			openFileDialog.Filter = "Dune Savegame|*.SAV";
			openFileDialog.Multiselect = false;
			openFileDialog.Title = "Open Dune Savegame";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				isLoading = true;
				FileName = openFileDialog.FileName;
				Exception result = new Exception(Conversions.ToString(1));
				Savegame = new Savegame_Item(ref result, FileName);
				if (Conversions.ToDouble(result.Message) == 1.0)
				{
					PopulateSietchList(Savegame.Uncompressed);
					PopulateGenerals(Savegame.Uncompressed);
					PopulateTroops(Savegame.Uncompressed);
				}
				else
				{
					MessageBox.Show("Result: " + result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				btnSave.Enabled = Conversions.ToBoolean(Interaction.IIf(Conversions.ToDouble(result.Message) == 1.0, true, false));
				TabControl.Enabled = Conversions.ToBoolean(Interaction.IIf(Conversions.ToDouble(result.Message) == 1.0, true, false));
			}
			isLoading = false;
		}

		private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Exception Result = new Exception(Conversions.ToString(1));
			General.Update(Conversions.ToInteger(txtSpice.Text), Conversions.ToInteger(txtContactDistance.Text), Conversions.ToByte(txtCharisma.Text));
			Savegame.SaveCompressed(ref Result);
			if (Conversions.ToDouble(Result.Message) == 1.0)
			{
				MessageBox.Show("Savegame was saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show(Result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}
