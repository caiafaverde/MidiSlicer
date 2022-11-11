using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourByFour
{
	using Ins = KeyValuePair<string, byte>;
	public partial class BeatControl : UserControl
	{
		static IList<bool> _rythmClipboard;
		
		static readonly object _DeleteKey = new object();
		static readonly object _BarsChangedKey = new object();
		static readonly object _NoteIdChangedKey = new object();

		public byte Channel { get; set; }

		public BeatControl() : this(1, 16) { }
		public BeatControl(int bars, int stepCount)
		{
			InitializeComponent();
			Channel = 9;
			StepControl.BuildSteps(bars, stepCount);
			var items = Instrument.Items;
			items.Add(new Ins("Acoustic Bass Drum",35));
			items.Add(new Ins("Bass Drum 1",36));
			items.Add(new Ins("Rim Shot (Side Stick)",37));
			items.Add(new Ins("Acoustic Snare",38));
			items.Add(new Ins("Hand Clap",39));
			items.Add(new Ins("Electric Snare",40));
			items.Add(new Ins("Low Tom A",41));
			items.Add(new Ins("Closed Hi-Hat",42));
			items.Add(new Ins("Low Tom B",43));
			items.Add(new Ins("Pedal Hi-Hat",44));
			items.Add(new Ins("Mid Tom A",45));
			items.Add(new Ins("Open Hi-Hat",46));
			items.Add(new Ins("Mid Tom B",47));
			items.Add(new Ins("High Tom A",48));
			items.Add(new Ins("Crash Cymbal 1",49));
			items.Add(new Ins("High Tom B",50));
			items.Add(new Ins("Ride Cymbal 1",51));
			items.Add(new Ins("Chinese Cymbal",52));
			items.Add(new Ins("Ride Bell",53));
			items.Add(new Ins("Tambourine",54));
			items.Add(new Ins("Splash Cymbal",55));
			items.Add(new Ins("Cowbell",56));
			items.Add(new Ins("Crash Cymbal 2",57));
			items.Add(new Ins("Vibraslap",58));
			items.Add(new Ins("Ride Cymbal 2",59));
			items.Add(new Ins("Hi Bongo",60));
			items.Add(new Ins("Low Bongo",61));
			items.Add(new Ins("Mute Hi Conga",62));
			items.Add(new Ins("Open Hi Conga",63));
			items.Add(new Ins("Low Conga",64));
			items.Add(new Ins("High Timbale",65));
			items.Add(new Ins("Low Timbale",66));
			items.Add(new Ins("High Agogo",67));
			items.Add(new Ins("Low Agogo",68));
			items.Add(new Ins("Cabasa",69));
			items.Add(new Ins("Maracas",70));
			items.Add(new Ins("Short Whistle",71));
			items.Add(new Ins("Long Whistle",72));
			items.Add(new Ins("Short Guiro",73));
			items.Add(new Ins("Long Guiro",74));
			items.Add(new Ins("Claves",75));
			items.Add(new Ins("Hi Wood Block",76));
			items.Add(new Ins("Low Wood Block",77));
			items.Add(new Ins("Mute Cuica",78));
			items.Add(new Ins("Open Cuica",79));
			items.Add(new Ins("Mute Triangle",80));
			items.Add(new Ins("Open Triangle ",81));
			Instrument.SelectedIndex = 0;
		}
		public event EventHandler Delete {
			add { Events.AddHandler(_DeleteKey, value); }
			remove { Events.RemoveHandler(_DeleteKey, value); }
		}
		public byte NoteId {
			get {
				return ((Ins)Instrument.SelectedItem).Value;
			}
			set {
				for(int ic = Instrument.Items.Count,i=0;i<ic;++i)
				{
					var item = (Ins)Instrument.Items[i];
					if(item.Value==value)
					{
						Instrument.SelectedIndex = i;
						OnNoteIdChanged(EventArgs.Empty);
						return;
					}
				}
				throw new InvalidOperationException("The note is not part of the kit");
			}
		}
		protected void OnNoteIdChanged(EventArgs args)
		{
			(Events[_NoteIdChangedKey] as EventHandler)?.Invoke(this, args);
		}
		public event EventHandler NoteIdChanged {
			add { Events.AddHandler(_NoteIdChangedKey, value); }
			remove { Events.RemoveHandler(_NoteIdChangedKey, value); }
		}
		public int Bars {
			get {
				return StepControl.Bars;
			}
			set {
				StepControl.Bars = value;
				OnBarsChanged(EventArgs.Empty);
				StepControl.Size = new Size(StepControl.Width, Height);
			}
		}

		public int StepCount
		{
			get
			{
				return StepControl.StepCount;
			}
			set
			{
				StepControl.StepCount = value;
				//OnBarsChanged(EventArgs.Empty);
				StepControl.Size = new Size(StepControl.Width, Height);
			}
		}

		protected void OnBarsChanged(EventArgs args)
		{
			(Events[_BarsChangedKey] as EventHandler)?.Invoke(this, args);
		}
		public event EventHandler BarsChanged {
			add { Events.AddHandler(_BarsChangedKey, value); }
			remove { Events.RemoveHandler(_BarsChangedKey, value); }
		}
		public IList<bool> Steps {
			get { return StepControl.Steps; }
		}
		protected void OnDelete(EventArgs args)
		{
			(Events[_DeleteKey] as EventHandler)?.Invoke(this, args);
		}

		private void MenuButton_Click(object sender, EventArgs e)
		{
			this.contextMenuStrip1.Show(Control.MousePosition);
		}

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
			this.channelToolStripMenuItem.Text = $"Channel {Channel + 1}";
			
			this.pasteToolStripMenuItem.Enabled = (_rythmClipboard != null);
			//fill Generate subitems
			this.generateToolStripMenuItem.DropDownItems.Clear();

			//Add Simple E rythms ;2..Count for relatively prime numbers
			//also might be interesting for half-intervals
			for (int j = 3; j <= this.StepControl.StepCount; j++)
			{
				//comment this if interested in odd rythms
				if (this.StepControl.StepCount % j != 0) 
					continue;

				for (int i = 2; i < j; i++)
				{
					if (Rythm.GCD(i, j) != 1)
						continue;
					AddEuclRythmMenuItem(i, j);
				}
			}
			//TODO - mayde add sums of smaller mutially prime-based intervals, e.g. for 16 E(x,7)+E(y,9)
			//Or move that to custom rythm generator
        }

		private void AddEuclRythmMenuItem(int a, int b)
        {
			var rythm = Rythm.Euclidean(a, b);
			var tsmi = new ToolStripMenuItem();
			tsmi.Text = $"E({a}, {b})";
			tsmi.Tag = rythm;
			tsmi.Click += AddRythmFromEucl_Click;
			this.generateToolStripMenuItem.DropDownItems.Add(tsmi);
		}

        private void AddRythmFromEucl_Click(object sender, EventArgs e)
        {
           if (sender is ToolStripMenuItem)
            {
				var tag = ((ToolStripMenuItem)sender).Tag;
				if (tag is Rythm)
					FillStepsWith((tag as Rythm).Steps);

			}
        }

        private void FillStepsWith(IList<bool> data)
        {
			for (int i = 0, j = 0; i < this.StepControl.StepCount * this.StepControl.Bars; i++, j++)
			{
				if (j == data.Count)
					j = 0;
				this.Steps[i] = data[j];
			}
		}

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
			FillStepsWith(new bool[this.StepControl.StepCount * this.StepControl.Bars]);

		}

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
			//Do we really need this one now?
			OnDelete(EventArgs.Empty);
		}

        private void stepLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var rythm = Rythm.FromBool(this.Steps);
			var rythmStr = rythm.ToString();
			var newrythm = string.Concat(rythmStr.Substring(1), rythmStr[0]);
			FillStepsWith(Rythm.FromBeatString(newrythm).Steps);
		}

        private void stepRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var rythm = Rythm.FromBool(this.Steps);
			var rythmStr = rythm.ToString();
			var newrythm = string.Concat(rythmStr[rythmStr.Length - 1], rythmStr.Substring(0, rythmStr.Length -1));
			FillStepsWith(Rythm.FromBeatString(newrythm).Steps);
		}

        private void intervalLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var rythm = Rythm.FromBool(this.Steps);
			var rythmStr = rythm.ToIntervalString();
			if (string.IsNullOrWhiteSpace(rythmStr))
				return;
			if (rythmStr.Length == 1)
				return;
			var newrythm = string.Concat(rythmStr.Substring(1), rythmStr[0]);
			FillStepsWith(Rythm.FromIntervalString(newrythm).Steps);
		}

        private void intervalRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var rythm = Rythm.FromBool(this.Steps);
			var rythmStr = rythm.ToIntervalString();
			if (string.IsNullOrWhiteSpace(rythmStr))
				return;
			if (rythmStr.Length == 1)
				return;
			var newrythm = string.Concat(rythmStr[rythmStr.Length - 1], rythmStr.Substring(0, rythmStr.Length - 1));
			FillStepsWith(Rythm.FromIntervalString(newrythm).Steps);
		}

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_rythmClipboard = this.Steps;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
			FillStepsWith(_rythmClipboard);
			_rythmClipboard = null;
		}
    }
}
