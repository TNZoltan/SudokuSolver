using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Run();
        }
        private readonly bool[,,] _table = new bool[10,10,10]; //skip zero
        private object key = new object();

        private void Run(){
            //Initialize
            for (int i = 1; i <= 9; ++i){
                for (int j = 1; j <= 9; ++j) {
                    _table[i, j, 0] = false;
                    for (int l = 1; l <= 9; ++l){
                        _table[i,j,l] = true;
                    }
                }
            }
            //Solve
            int row = 1, column = 1;
            while (Done()) {
                if (Number(row, column) == -1) {
                    RowCheck(row, column);
                    ColumnCheck(row, column);
                    SegmentCheck(row, column);
                }
                if (column != 9) ++column;
                else if (row!=9) {
                    column = 1;
                    row++;
                }
                else {
                    row = 1;
                    column = 1;
                }

            }
        }

        private void  RowCheck(int r, int c){
            int temp;
            for (int i=1; i<=9; ++i) {
                temp = Number(r, i);
                for (int j=1; j<=9; ++j) {
                    if (temp == j) _table[r, c, j] = false;
                }
            }
        }

        private void ColumnCheck(int r, int c){
            int temp;
            for (int i=1; i<= 9; ++i) {
                temp = Number(i, c);
                for (int j=1; j<=9; ++j) {
                    if (temp == j) _table[r, c, j] = false;
                }
            }
        }

        private void SegmentCheck(int r, int c){
            int shiftR = 0;
            if (r >= 1 && r <= 3) shiftR = -3; if (r >= 7 && r <= 9) shiftR = 3;
            int shiftC = 0;
            if (c >= 1 && c <= 3) shiftC = -3; if (c >= 7 && c <= 9) shiftC = 3;
            int temp;
            for (int i = 4; i <= 6; ++i) {
                for (int j = 4; j <= 6; ++j) {
                    temp = Number((i+ shiftR),(j+ shiftC));
                    for (int j = 1; j <= 9; ++j) {
                        if (temp == j) _table[r, c, j] = false;
                    }
                }
            }
        }
        private int Number(int r, int c) {
            int count = 0;
            for (int i = 1; i <= 9; ++i){
                if (_table[r, c, i]) count++;
            }
            if (count > 1) return -1;
            for (int i=1; i<=9; ++i) {
                if (_table[r,c,i]) return i;
            }
            return -1;
        }
        
        private void InsertNumber(int r, int c, int num) {
            if (num > 0 && num <= 9) {
                for (int i = 1; i <= 9; ++i) {
                    if (!(i == num)) _table[r, c, i] = false;
                }
            }
        }
        private bool Done() {
            int count = 0;
            for (int i = 1; i <= 9; ++i) {
                for (int j = 1; j <= 9; ++j) {
                    for (int l = 1; l <= 9; ++l) {
                        if (_table[i, j, l]) count++;
                    }
                }
            }
            if (count == 81) return true;
            return false;
        }
    }
}
