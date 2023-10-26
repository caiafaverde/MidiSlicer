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
		//_ProbList _probs;
		//_SubStepList _subSteps;
		
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

			//_probs = new _ProbList(bars*stepCount);
            //_subSteps = new _SubStepList(bars * stepCount);


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

                        //ch.MouseClick += Ch_MouseClick;
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

        //private void Ch_MouseClick(object sender, MouseEventArgs e)
        //{
        //    ((sender as Control).Parent as StepControl).Probs[Convert.ToInt32((sender as Control).Tag)] = (sender as StepToggle).Probability;
        //    ((sender as Control).Parent as StepControl).SubSteps[Convert.ToInt32((sender as Control).Tag)] = (sender as StepToggle).SubSteps;
        //}

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

		//public IList<int> Probs => _probs;

		public IList<DrumStep> Steps { get { return _steps; } }

        //public IList<SubSteps> SubSteps { get { return _subSteps; } }


        private sealed class _StepList : IList<DrumStep>
		{
			readonly ControlCollection _controls;
			internal _StepList(ControlCollection controls)
			{
				_controls = controls;
			}
			public DrumStep this[int index] 
			{
				get {
					var ch = _controls[index] as StepToggle;
					return ch.DrumStep;
				}
				set {
					var ch = _controls[index] as StepToggle;
					ch.DrumStep = value;
				}

			}

			public int Count { get { return _controls.Count; } }
			public bool IsReadOnly { get { return false; } }

			public void Add(DrumStep item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public void Clear()
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Contains(DrumStep item)
			{
				for(int ic=_controls.Count,i = 0;i<ic;++i)
				{
					var ch = _controls[i] as StepToggle;
					if (item == ch.DrumStep)
						return true;
				}
				return false;
			}

			public void CopyTo(DrumStep[] array, int arrayIndex)
			{
				for(int ic=_controls.Count,i=0;i<ic; ++i)
					array[arrayIndex + i] = (_controls[i] as StepToggle).DrumStep;
			}

			public IEnumerator<DrumStep> GetEnumerator()
			{
				for (int ic = _controls.Count, i = 0; i < ic; ++i)
					yield return (_controls[i] as StepToggle).DrumStep;
			}

			public int IndexOf(DrumStep item)
			{
				for (int ic = _controls.Count, i = 0; i < ic; ++i)
				{
					var ch = _controls[i] as StepToggle;
					if (item == ch.DrumStep)
						return i;
				}
				return -1;
			}

			public void Insert(int index, DrumStep item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Remove(DrumStep item)
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
		/*
		private sealed class _ProbList : IList<int>
		{
            int[] _back;
			
			//readonly ControlCollection _controls;
			internal _ProbList(int cnt)
			{
				//_controls = controls;
				_back = new int[cnt];
			}
			public int this[int index]
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

			public void Add(int item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public void Clear()
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Contains(int item)
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
				{
					//var ch = _controls[i] as StepToggle;
					if (item == _back[i])
						return true;
				}
				return false;
			}

			public void CopyTo(int[] array, int arrayIndex)
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
					array[arrayIndex + i] = _back[i];
			}

			public IEnumerator<int> GetEnumerator()
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
					yield return _back[i];
			}

			public int IndexOf(int item)
			{
				for (int ic = _back.Length, i = 0; i < ic; ++i)
				{
					//var ch = _controls[i] as StepToggle;
					if (item == _back[i])
						return i;
				}
				return -1;
			}

			public void Insert(int index, int item)
			{
				throw new NotSupportedException("The list is fixed size");
			}

			public bool Remove(int item)
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

        private sealed class _SubStepList : IList<SubSteps>
        {
            SubSteps[] _back;

            //readonly ControlCollection _controls;
            internal _SubStepList(int cnt)
            {
                //_controls = controls;
                _back = new SubSteps[cnt];
            }
            public SubSteps this[int index]
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

            public void Add(SubSteps item)
            {
                throw new NotSupportedException("The list is fixed size");
            }

            public void Clear()
            {
                throw new NotSupportedException("The list is fixed size");
            }

            public bool Contains(SubSteps item)
            {
                for (int ic = _back.Length, i = 0; i < ic; ++i)
                {
                    //var ch = _controls[i] as StepToggle;
                    if (item == _back[i])
                        return true;
                }
                return false;
            }

            public void CopyTo(SubSteps[] array, int arrayIndex)
            {
                for (int ic = _back.Length, i = 0; i < ic; ++i)
                    array[arrayIndex + i] = _back[i];
            }

            public IEnumerator<SubSteps> GetEnumerator()
            {
                for (int ic = _back.Length, i = 0; i < ic; ++i)
                    yield return _back[i];
            }

            public int IndexOf(SubSteps item)
            {
                for (int ic = _back.Length, i = 0; i < ic; ++i)
                {
                    //var ch = _controls[i] as StepToggle;
                    if (item == _back[i])
                        return i;
                }
                return -1;
            }

            public void Insert(int index, SubSteps item)
            {
                throw new NotSupportedException("The list is fixed size");
            }

            public bool Remove(SubSteps item)
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

		*/
    }
}
