using Eto.Forms;
using Eto.Drawing;
using Chess.UI.Eto.Controls;
using Chess.Core;

namespace Chess.UI.Eto;

public partial class MainForm : Form
{
		private ChessboardControl _boardControl;
		private TextBox _txtFen;
		private TextBox _txtSelectedSquare;
		private Button _btnLoadFen;

		private ChessGame _chessGame;

	public MainForm()
	{
		_chessGame = new ChessGame();
		//chessBoard = new Board();

		Title = "Chess Engine";
		this.ClientSize = new Size(1600, 900);

		_boardControl = new ChessboardControl(_chessGame.currentChessPosition, new Size(700, 700));
		_txtFen = new TextBox { Text = "", Width = 600 };
		_btnLoadFen = new Button { Text = "Load", Width = 90 };

		_txtSelectedSquare = new TextBox { Text = "", Width= 90 };

		var sideLayout = new StackLayout
		{
			Orientation = Orientation.Vertical,
			Spacing = 5,
			HorizontalContentAlignment = HorizontalAlignment.Left,
			Items =
			{
				new Label { Text = "FEN"},
				_txtFen,
				_btnLoadFen,
				_txtSelectedSquare
			}
		};

		_btnLoadFen.Click += btnLoadFen_Click;

		Content = new TableLayout
		{
			Spacing = new Size(5, 5),
			Padding = new Padding(25, 25, 25, 25),
			Rows =
			{
				new TableRow(
						new TableCell(_boardControl, false),
						new TableCell(sideLayout)
					),
			}

		};

		var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
		quitCommand.Executed += (sender, e) => Application.Instance.Quit();

		var aboutCommand = new Command { MenuText = "About..." };
		aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

		// create menu
		Menu = new MenuBar
		{
			Items =
			{
				// File submenu
				// new SubMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
				// new SubMenuItem { Text = "&View", Items = { /* commands/items */ } },
			},
			ApplicationItems =
			{
				// application (OS X) or file menu (others)
				new ButtonMenuItem { Text = "&Preferences..." },
			},
			QuitItem = quitCommand,
			AboutItem = aboutCommand
		};

		// create toolbar			
		//ToolBar = new ToolBar { Items = { clickMe } };
	}

	private void btnLoadFen_Click(object? sender, EventArgs e)
	{
		string fen = _txtFen.Text?.Trim() ?? "";

		if (string.IsNullOrWhiteSpace(fen))
		{
			MessageBox.Show("Please enter a FEN string.", "Missing FEN", MessageBoxType.Warning);
			return;
		}

		try
		{
			_chessGame = new ChessGame();
			_chessGame.currentChessPosition.LoadFenPosition(fen);
			_boardControl.UpdateChessPosition(_chessGame.currentChessPosition);
			_boardControl.Invalidate();
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Error loading FEN:\n{ex.Message}", "Error", MessageBoxType.Error);
		}
	}

}