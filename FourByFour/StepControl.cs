using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace FourByFour
{
	public partial class StepControl : UserControl
	{
		static readonly object _BarsChangedKey = new object();
		static readonly object _StepCountChangedKey = new object();
		_StepList _steps;
		_ProbList _probs;
		
		int _bars;
		int _stepCount = 16;
		public StepControl()
		{
			_bars = 1;
			//_BuildSteps(_bars);
			InitializeComponent();
		}
		public void BuildSteps(int bars, int stepCount)
		{
			_stepCount = stepCount;
			_bars = bars;
			Controls.Clear();
			StepToggle ch;
			var left = 0;

			_probs = new _ProbList(bars*stepCount);

			for (var k = 0; k < bars; ++k)
			{
				for (var i = 0; i < stepCount; ++i)
				{
					//for (var j = 0; j < 4; ++j)
					{
						ch = new StepToggle();
						Controls.Add(ch);
						//ch.Appearance = Appearance.Button;
						//ch.Size = new Size(16, Height);
						ch.ActiveLedColor = Color.Yellow;
						ch.InactiveLedColor = Color.Gray;
						ch.Bevel.InnerBorder = true;
						ch.Bevel.OutterBorder = true;
						ch.Bevel.Style = BevelStyle.FrameRaised;

						ch.Location = new Point(left, 0);
						ch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
						left += ch.Size.Width;

                        ch.MouseClick += Ch_MouseClick;
						ch.Tag = k * 4 + i;
					}
					if ((i + k + 1)%4== 0)
						left += 4;
				}
			}
			_steps = new _StepList(Controls);
			
			
			Size = new Size(left, 26);
			MinimumSize = new Size(left, 26);
		}

        private void Ch_MouseClick(object sender, MouseEventArgs e)
        {
            ((sender as Control).Parent as StepControl).Probs[Convert.ToInt32((sender as Control).Tag)] = (sender as StepToggle).Probability;
        }

        public int Bars {
			get {
				return _bars;
			}
			set {
				if (_bars != value)
				{
					_bars = value;
					BuildSteps(_bars, _stepCount);
					OnBarsChanged(EventArgs.Empty);
				}
			}
		}

		public int StepCount
		{
			get
			{
				return _stepCount;
			}
			set
			{
				if (_stepCount != value)
				{
					_stepCount = value;
					BuildSteps(_bars, _stepCount);
					OnStepCountChanged(EventArgs.Empty);
				}
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

		protected void OnStepCountChanged(EventArgs args)
		{
			(Events[_StepCountChangedKey] as EventHandler)?.Invoke(this, args);
		}
		public event EventHandler StepCountChanged
		{
			add { Events.AddHandler(_StepCountChangedKey, value); }
			remove { Events.RemoveHandler(_StepCountChangedKey, value); }
		}

		public IList<float> Probs => _probs;

		public IList<bool> Steps { get { return _steps; } }


		private sealed class _StepList : IList<bool>
		{
			readonly ControlCollection _controls;
			internal _StepList(ControlCollection controls)
			{
				_controls = controls;
			}
			public bool this[int index] 
			{
				get {
					var ch = _controls[index] as StepToggle;
					return ch.Checked;
				}
				set {
					var ch = _controls[index] as StepToggle;
					ch.Checked = value;
				}

			}

			public int Count { get { return _controls.Count; } }
			public bool IsReadOnly { get { return false; } }

			public void Add(bool item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public void Clear()
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Contains(bool item)
			{
				for(int ic=_controls.Count,i = 0;i<ic;++i)
				{
					var ch = _controls[i] as StepToggle;
					if (item == ch.Checked)
						return true;
				}
				return false;
			}

			public void CopyTo(bool[] array, int arrayIndex)
			{
				for(int ic=_controls.Count,i=0;i<ic; ++i)
					array[arrayIndex + i] = (_controls[i] as StepToggle).Checked;
			}

			public IEnumerator<bool> GetEnumerator()
			{
				for (int ic = _controls.Count, i = 0; i < ic; ++i)
					yield return (_controls[i] as StepToggle).Checked;
			}

			public int IndexOf(bool item)
			{
				for (int ic = _controls.Count, i = 0; i < ic; ++i)
				{
					var ch = _controls[i] as StepToggle;
					if (item == ch.Checked)
						return i;
				}
				return -1;
			}

			public void Insert(int index, bool item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Remove(bool item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public void RemoveAt(int index)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private sealed class _ProbList : IList<float>
		{
			float[] _back;
			
			//readonly ControlCollection _controls;
			internal _ProbList(int cnt)
			{
				//_controls = controls;
				_back = new float[cnt];
			}
			public float this[int index]
			{
				get
				{
					return _back[index];
					//var ch = _controls[index] as StepToggle;
					//return ch.Probability;
				}
				set
				{
					//var ch = _controls[index] as StepToggle;
					//ch.Probability = value;
					_back[index] = value;
				}

			}

			public int Count { get { return _back.Length; } }
			public bool IsReadOnly { get { return false; } }

			public void Add(float item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public void Clear()
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Contains(float item)
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
				{
					//var ch = _controls[i] as StepToggle;
					if (item == _back[i])
						return true;
				}
				return false;
			}

			public void CopyTo(float[] array, int arrayIndex)
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
					array[arrayIndex + i] = _back[i];
			}

			public IEnumerator<float> GetEnumerator()
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
					yield return _back[i];
			}

			public int IndexOf(float item)
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
				{
					//var ch = _controls[i] as StepToggle;
					if (item == _back[i])
						return i;
				}
				return -1;
			}

			public void Insert(int index, float item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Remove(float item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public void RemoveAt(int index)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}
	}
}
