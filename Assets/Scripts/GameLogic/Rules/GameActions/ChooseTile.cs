using EDBG.GameLogic.MapSystem;
using EDBG.States;

namespace EDBG.GameLogic.Rules
{
    public struct ChooseTile
    {
		private MapTile _selectedTile;

		public MapTile SelectedTile
		{
			get { return _selectedTile; }
			private set { _selectedTile = value; }
		}

		private LogicState _logicState;

		public LogicState LogicState
		{
			get { return _logicState; }
			private set { _logicState = value; }
		}

		public ChooseTile(MapTile selectedTile, LogicState state)
		{
			_selectedTile = selectedTile.Clone() as MapTile;
			_logicState = state;
		}

		public void UpdateState(LogicState newState)
		{
            _logicState = newState;
            _selectedTile = _logicState.MapGrid.GetCell(_selectedTile.GamePosition) as MapTile;
			
		}
	}
}