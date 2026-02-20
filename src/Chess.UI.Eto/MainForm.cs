using System;
using Eto.Forms;
using Eto.Drawing;
using Chess.UI.Eto.Controls;
using Chess.Core;

namespace Chess.UI.Eto;

public partial class MainForm : Form
{
		private ChessboardControl board;

		private TextBox txtFen;
		private TextBox txtSelectedSquare;
		private Button btnLoadFen;

		private Board chessBoard;
	


	public MainForm()
	{
		chessBoard = new Board();

		Title = "Chess Engine";
		this.ClientSize = new Size(1600, 900);

		board = new ChessboardControl(chessBoard, new Size(700, 700));
		txtFen = new TextBox { Text = "", Width = 600 };
		btnLoadFen = new Button { Text = "Load", Width = 90 };

		txtSelectedSquare = new TextBox { Text = "", Width= 90 };

		var sideLayout = new StackLayout
		{
			Orientation = Orientation.Vertical,
			Spacing = 5,
			HorizontalContentAlignment = HorizontalAlignment.Left,
			Items =
			{
				new Label { Text = "FEN"},
				txtFen,
				btnLoadFen,
				txtSelectedSquare
			}
		};

		btnLoadFen.Click += btnLoadFen_Click;

		Content = new TableLayout
		{
			Spacing = new Size(5, 5),
			Padding = new Padding(25, 25, 25, 25),
			Rows =
			{
				new TableRow(
						new TableCell(board, false),
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
		string fen = txtFen.Text?.Trim() ?? "";

		if (string.IsNullOrWhiteSpace(fen))
		{
			MessageBox.Show("Please enter a FEN string.", "Missing FEN", MessageBoxType.Warning);
			return;
		}

		try
		{
			chessBoard.LoadFenPosition(fen);
			Invalidate();
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Error loading FEN:\n{ex.Message}", "Error", MessageBoxType.Error);
		}
	}

}