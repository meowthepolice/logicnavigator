
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Logic_Navigator
{
    public class frmMChild_Chess : System.Windows.Forms.Form
    {
        private SolidBrush HighlightBrush = new SolidBrush(Color.HotPink);
        private Pen HighlightPen = new Pen(Color.HotPink);

        Graphics eGfx;


        private Font drawFont = new Font("Tahoma", 11, FontStyle.Regular);
        StringFormat drawFormat = new StringFormat();
        
		private System.IO.StreamReader SR;
        ArrayList ECOCodeList = new ArrayList();

        Image WhitePawn, WhiteBishop, WhiteRook, WhiteKnight, WhiteKing, WhiteQueen;
        Image BlackPawn, BlackBishop, BlackRook, BlackKnight, BlackKing, BlackQueen;
        Point offset = new Point(0, 0); bool piecegrabbed = false; Point mouseposition = new Point(0, 0); int draggedpiece = 0;
        Point updatesquares = new Point(0, 0);
        string View = "White";
        int turn = Black;
        private string moverecord = "";
        private string moverecordECO = "";
        private string ECOmovesavailable = "";
        private string ECOmovesavailableCodes = "";
        private string bestmoves = "";       private string analysis = "";
        int[] timestamps = new int[8] {0,0,0,0,0,0,0,0};
        int[,] line = new int[5,4] { {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}};
        private bool autoplay = false;
        private bool moving = false;
        private int AI1whitewins = 0, AI1blackwins = 0, stalemates = 0, AI2whitewins = 0, AI2blackwins = 0;
        Random randObj = new Random();
        private int games = 1;
        private int stalematecounter = 0;
        private bool checkmate = false; string winner;
       // private int movenumber = 0;
        private bool endgame = false;
        private bool endendgame = false;
        private bool middlegame = false;
        private int winningamount = 0;
        private int movetime;
        private int AI1_totaltime = 0, AI2_totaltime = 0;
        private bool AI1_Quiesce, AI2_Quiesce;
        private bool AI1_PST, AI2_PST;
        private bool AI1_ItD, AI2_ItD;
        private bool AI1_NS, AI2_NS;
        private bool AI2_MoveorderingD1, AI2_MoveorderingD2;
        private int bestAlpha, bestBeta;
        private bool AI1_KH, AI2_KH;
        private int AI1_QDepth, AI2_QDepth;
        private bool addrandom;
        private int loopcount = 0;
        private int loopcount2 = 0;

        StateofPlay gamestate;

        public const int White = 0;
        public const int Black = 1;
        public const int AI1 = 0;
        public const int AI2 = 1;

        const int WhiteTurn = 0;
        const int BlackTurn = 1;
        private int movenumber = 0;
        private int cummulativemoves = 1;
        private string timestamp;
        DateTime StopWatch;
        private Point lastmove_start;
        private Point lastmove_finish;
        private Point secondlastmove_start;
        private Point secondlastmove_finish;
        private Point blacklastmove_start, blacklastmove_finish, whitelastmove_start, whitelastmove_finish;
        private Point Startconsidered = new Point(0,0); 
        private Point Finishconsidered = new Point(0,0);
        private Point killerstart = new Point(0,0); 
        private Point killerend = new Point(0,0);
        
        private bool thinking = false;
        private ArrayList customerArray = new ArrayList();
        private int score = 0;
        private int pointstally = 0;
        ArrayList WhitePiecesTaken = new ArrayList();
        ArrayList BlackPiecesTaken = new ArrayList();
        ArrayList Scoregraph = new ArrayList();
        ArrayList GameBoard = new ArrayList();
        ArrayList GameSOP = new ArrayList();
        private int currentAI;

        private const int Empty = 0;

        private const int Black_King = 1;
        private const int Black_Queen = 2;
        private const int Black_Rook = 3;
        private const int Black_Bishop = 4;
        private const int Black_Knight = 5;
        private const int Black_Pawn = 6;

        private const int White_King = 7;
        private const int White_Queen = 8;
        private const int White_Rook = 9;
        private const int White_Bishop = 10;
        private const int White_Knight = 11;
        private const int White_Pawn = 12;
        int[] mobilityfactor = new int[13] {0,    0, -5, -5, -3, -3, 1,    0, 5, 5, 3, 3, 1};

        int[,] ChessPositions = new int[8, 8] { 
         { White_Rook, White_Knight, White_Bishop, White_Queen, White_King, White_Bishop, White_Knight, White_Rook },
         { White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn },
         { Black_Rook, Black_Knight, Black_Bishop, Black_Queen, Black_King, Black_Bishop, Black_Knight, Black_Rook }};


        int[,] ChessPositions1 = new int[8, 8] { 
         { Empty, Empty, Empty, White_King, Empty, Empty, Empty, Empty },
         { Empty, White_Pawn, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Black_Pawn, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Black_King, Empty, Empty, Empty, Empty }};


        //int[,] ChessPositions = new int[8, 8] { 
        // { White_Rook, White_Knight, White_Bishop, White_Queen, White_King, White_Bishop, Empty, White_Rook },
        // { White_Pawn, White_Pawn, White_Pawn, White_Pawn, Empty, White_Pawn, White_Pawn, White_Pawn },
        // { Empty, Empty, Empty, Empty, Empty, White_Knight, Empty, Empty },
        // { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
        // { Empty, Empty, Black_Knight, Empty, White_Pawn, Empty, Empty, Empty },
        // { Empty, Empty, Black_Knight, Empty, Empty, Empty, Empty, Empty },
        // { Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn },
        // { Black_Rook, Empty, Black_Bishop, Black_Queen, Black_King, Black_Bishop, Empty, Black_Rook }};
        
        /*int[,] ChessPositions = new int[8, 8] { 
         { White_Rook, Empty, Empty, Empty, White_King, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },         
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },         
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Black_King, Empty, Empty, Empty, Empty }};*/
        int[,] ChessPositionsbeingconsidered = new int[8, 8] { 
         { White_Rook, White_Knight, White_Bishop, White_Queen, White_King, White_Bishop, White_Knight, White_Rook },
         { White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn },
         { Black_Rook, Black_Knight, Black_Bishop, Black_Queen, Black_King, Black_Bishop, Black_Knight, Black_Rook }};
        int[,] CustomBoard = new int[8, 8] { 
         { White_Rook, White_Knight, White_Bishop, White_Queen, White_King, White_Bishop, White_Knight, White_Rook },
         { White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn, White_Pawn },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
         { Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn, Black_Pawn },
         { Black_Rook, Black_Knight, Black_Bishop, Black_Queen, Black_King, Black_Bishop, Black_Knight, Black_Rook }};


        int[,] CastlingPositions = new int[8, 8] { 
            { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};
        
        int[,] ValidMovesPositions = new int[8, 8] { 
            { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};

        bool[,] Zerosglobal = new bool[8, 8] {  
            {false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
        
        int[,] EdgeDistance = new int[8, 8] { 
         { 1,1,1,1,1,1,1,1},
         { 1,2,2,2,2,2,2,1},
         { 1,2,3,3,3,3,2,1},
         { 1,2,3,4,4,3,2,1},
         { 1,2,3,4,4,3,2,1},
         { 1,2,3,3,3,3,2,1},
         { 1,2,2,2,2,2,2,1},
         { 1,1,1,1,1,1,1,1}};

        /*
        int[,] Pawn_PieceSquareTable1 = new int[8, 8] { 
         {0,  0,  0,  0,  0,  0,  0,  0},
         {50, 50, 50, 50, 50, 50, 50, 50},
         {10, 10, 20, 30, 30, 20, 10, 10},
         {5,  5, 10, 25, 25, 10,  5,  5},
         {0,  0,  0, 20, 20,  0,  0,  0},
         {5, -5,-10,  0,  0,-10, -5,  5},
         {5, 10, 10,-20,-20, 10, 10,  5},
         {0,  0,  0,  0,  0,  0,  0,  0}};

        int[,] Knight_PieceSquareTable1 = new int[8, 8] { 
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}};

        int[,] Bishop_PieceSquareTable1 = new int[8, 8] { 
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}};

        int[,] Rook_PieceSquareTable1 = new int[8, 8] {
        {0,  0,  0,  0,  0,  0,  0,  0},
        {5, 10, 10, 10, 10, 10, 10,  5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        { 0,  0,  0,  5,  5,  0,  0,  0}};

        int[,] Queen_PieceSquareTable1 = new int[8, 8] {
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        { -5,  0,  5,  5,  5,  5,  0, -5},
        {  0,  0,  5,  5,  5,  5,  0, -5},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}};

        int[,] King_PieceSquareTable_middlegame1 = new int[8, 8] {
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        { 20, 20,  0,  0,  0,  0, 20, 20},
        { 20, 30, 10,  0,  0, 10, 30, 20}};

        int[,] King_PieceSquareTable_endgame1 = new int[8, 8] {
        {-50,-40,-30,-20,-20,-30,-40,-50},
        {-30,-20,-10,  0,  0,-10,-20,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-30,  0,  0,  0,  0,-30,-30},
        {-50,-30,-30,-30,-30,-30,-30,-50}};
        */

        int[,] Pawn_PieceSquareTable = new int[8, 8] { 
         {0,  0, 0, 0, 0, 0, 0, 0},
         {10,10,10,10,10,10,10,10},
         { 2, 2, 4, 6, 6, 4, 2, 2},
         { 1, 1, 4, 5, 5, 2, 1, 1},
         { 0, 0, 0, 4, 4, 0, 0, 0},
         { 1,-1,-2, 0, 0,-2,-1, 1},
         { 1, 2, 2,-4,-4, 2, 2, 1},
         { 0, 0, 0, 0, 0, 0, 0, 0}};

        int[,] PawnBlitz_PieceSquareTable = new int[8, 8] { 
         {50, 50, 50, 50, 50, 50, 50, 50},
         {30,30,30,30,30,30,30,30},
         { 10, 10, 15, 20, 20, 15, 10, 10},
         { 1, 1, 4, 5, 5, 2, 1, 1},
         { 0, 0, 0, 4, 4, 0, 0, 0},
         { 1,-1,-2, 0, 0,-2,-1, 1},
         { 1, 2, 2,-4,-4, 2, 2, 1},
         { 0, 0, 0, 0, 0, 0, 0, 0}};

        int[,] Knight_PieceSquareTable = new int[8, 8] { 
        {-10,-8,-6,-6,-6,-6,-8,-10},
        { -8,-4, 0, 0, 0, 0,-4,-8},
        { -6, 0, 2, 3, 3, 2, 0,-6},
        { -6, 1, 3, 4, 4, 3, 1,-6},
        { -6, 0, 3, 4, 4, 3, 0,-6},
        { -6, 1, 2, 3, 3, 2, 1,-6},
        { -8,-4, 0, 1, 1, 0,-4,-8},
        {-10,-8,-6,-6,-6,-6,-8,-10}};
        
        int[,] Bishop_PieceSquareTable = new int[8, 8] { 
        {-4, -2, -2, -2, -2, -2, -2, -4},
        {-4,  0,  0,  0,  0,  0,  0, -4},
        {-2,  0,  1,  2,  2,  1,  0, -4},
        {-2,  1,  1,  2,  2,  1,  1, -2},
        {-2,  0,  2,  2,  2,  2,  0, -2},
        {-2,  2,  2,  2,  2,  2,  2, -2},
        {-2,  1,  0,  0,  0,  0,  1, -2},
        {-4, -2, -2, -2, -2, -2, -2, -4}};
        
        int[,] Rook_PieceSquareTable = new int[8, 8] {
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 1,  2,  2,  2,  2,  2,  2,  1},
        {-1,  0,  0,  0,  0,  0,  0, -1},
        {-1,  0,  0,  0,  0,  0,  0, -1},
        {-1,  0,  0,  0,  0,  0,  0, -1},
        {-1,  0,  0,  0,  0,  0,  0, -1},
        {-1,  0,  0,  0,  0,  0,  0, -1},
        { 0,  0,  0,  5,  5,  0,  0,  0}};

        int[,] Queen_PieceSquareTable_notmiddlegame = new int[8, 8] {
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  2,  2,  0,  0,  0},
        { 2,  1,  1,  5,  5,  1,  1,  2}};

        int[,] Queen_PieceSquareTable = new int[8, 8] {
        {-4,- 2, -2, -1, -1, -2, -2, -4},
        {-2,  0,  0,  0,  0,  0,  0, -2},
        {-2,  0,  1,  1,  1,  1,  0, -2},
        {-1,  0,  1,  1,  1,  1,  0, -1},
        { 0,  0,  1,  1,  1,  1,  0, -1},
        {-2,  1,  1,  1,  1,  1,  0, -2},
        {-2,  0,  0,  0,  0,  0,  0, -2},
        {-4, -2, -2, -1, -1, -2, -2, -4}};
        
        //int[,] Queen_PieceSquareTable_middlegame = new int[8, 8] {
        //{-4,- 2, -2, -1, -1, -2, -2, -4},
        //{-2,  0,  0,  0,  0,  0,  0, -2},
        //{-2,  0,  1,  1,  1,  1,  0, -2},
        //{-1,  0,  1,  1,  1,  1,  0, -1},
        //{ 0,  0,  1,  1,  1,  1,  0, -1},
        //{-2,  1,  1,  1,  1,  1,  0, -2},
        //{-2,  0,  0, -2, -2,  0,  0, -2},
        //{-6, -3, -3, -6, -6, -3, -3, -6}};

 //       int[,] Pawn_PieceSquareTableCombined = new int[8, 8] { 
 //        {100, 100, 100, 100, 100, 100, 100, 100},
 //        {110,110,110,110,110,110,110,110},
 //        { 102, 102, 104, 106, 106, 104, 102, 102},
 //        { 101, 101, 104, 105, 105, 102, 101, 101},
 //        { 100, 100, 100, 104, 104, 100, 100, 100},
 //        { 101, 99, 98, 100, 100, 98, 99, 101},
 //        { 101, 102, 102, 96, 96, 102, 102, 101},
 //        { 0, 0, 0, 0, 0, 0, 0, 0}};
       
 //       int[,] Knight_PieceSquareTableCombined = new int[8, 8] { 
 //       {-10,-8,-6,-6,-6,-6,-8,-10},
 //       { -8,-4, 0, 0, 0, 0,-4,-8},
 //       { -6, 0, 2, 3, 3, 2, 0,-6},
 //       { -6, 1, 3, 4, 4, 3, 1,-6},
 //       { -6, 0, 3, 4, 4, 3, 0,-6},
 //       { -6, 1, 2, 3, 3, 2, 1,-6},
 //       { -8,-4, 0, 1, 1, 0,-4,-8},
 //       {-10,-8,-6,-6,-6,-6,-8,-10}};

 //       int[,] Bishop_PieceSquareTableCombined = new int[8, 8] { 
 //       {326, 328, 328, 328, 328, 328, 328, 326},
 //       {326, 330, 330, 330, 330, 330, 330, 326},
 //       {328, 330, 329, 332, 332, 321, 330, 326},
 //       {328, 331, 331, 332, 332, 331, 331, 328},
 //       {328, 330, 332, 332, 332, 332, 330, 328},
 //       {328, 332, 332, 332, 332, 332, 332, 328},
 //       {328, 331, 330, 330, 330, 330, 331, 328},
 //       {326, 328, 328, 328, 328, 328, 328, 326}};
 ///*
        //int[,] Rook_PieceSquareTable = new int[8, 8] {
        //{ 0,  0,  0,  0,  0,  0,  0,  0},
        //{ 1,  2,  2,  2,  2,  2,  2,  1},
        //{-1,  0,  0,  0,  0,  0,  0, -1},
        //{-1,  0,  0,  0,  0,  0,  0, -1},
        //{-1,  0,  0,  0,  0,  0,  0, -1},
        //{-1,  0,  0,  0,  0,  0,  0, -1},
        //{-1,  0,  0,  0,  0,  0,  0, -1},
        //{ 0,  0,  0,  5,  5,  0,  0,  0}};

        //int[,] Queen_PieceSquareTable = new int[8, 8] {
        //{-4,-2,-2, -1, -1,-2,-2,-4},
        //{-2,  0,  0,  0,  0,  0,  0,-2},
        //{-2,  0,  1,  1,  1,  1,  0,-2},
        //{ -1,  0,  1,  1,  1,  1,  0, -1},
        //{  0,  0,  1,  1,  1,  1,  0, -1},
        //{-2,  1,  1,  1,  1,  1,  0,-2},
        //{-2,  0,  0,  0,  0,  0,  0,-2},
        //{-4,-2,-2, -1, -1,-2,-2,-4}};

        //*/


        int[,] King_PieceSquareTable_middlegame = new int[8, 8] {
        {-6,-8,-8,-10,-10,-8,-8,-6},
        {-6,-8,-8,-10,-10,-8,-8,-6},
        {-6,-8,-8,-10,-10,-8,-8,-6},
        {-6,-8,-8,-10,-10,-8,-8,-6},
        {-4,-6,-6,-8,-8,-6,-6,-4},
        {-2,-4,-4,-4,-4,-4,-4,-2},
        { 4, 4,  0,  0,  0,  0, 4, 4},
        { 4, 6, 2,  0,  0, 2, 6, 4}};

        int[,] King_PieceSquareTable_endgame = new int[8, 8] {
        {-2,-8,-6,-4,-4,-6,-8,-2},
        {-6,-4,-2,  0,  0,-2,-4,-6},
        {-6,-2, 4, 6, 6, 4,-2,-6},
        {-6,-2, 6, 8, 8, 6,-2,-6},
        {-6,-2, 6, 8, 8, 6,-2,-6},
        {-6,-2, 4, 6, 6, 4,-2,-6},
        {-6,-6,  0,  0,  0,  0,-6,-6},
        {-2,-6,-6,-6,-6,-6,-6,-2}};       

        int[,] ExplorerBoard = new int[8, 8] { 
            {1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1},{1,1,1,1,1,1,1,1}};

        Point square = new Point(0, 0), clicked = new Point(0, 0);

        ImageAttributes attr = new ImageAttributes();

        private Pen BluePen = new Pen(Color.Blue);
        private Pen RedPen = new Pen(Color.Red);
        private Pen BlackPen = new Pen(Color.Black);
        private Pen myWhitePen = new Pen(Color.White);
        private Pen myGreenPen = new Pen(Color.Green);
        private Pen myYellowPen = new Pen(Color.SandyBrown);
        private SolidBrush BlueBrush = new SolidBrush(Color.Blue);
        private SolidBrush RedBrush = new SolidBrush(Color.Red);
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);
        private SolidBrush BeigeBrush = new SolidBrush(Color.Beige);
        private SolidBrush BisqueBrush = new SolidBrush(Color.RosyBrown);
        private SolidBrush BrownBrush = new SolidBrush(Color.Brown);
        private SolidBrush WhiteBrush = new SolidBrush(Color.White);
        private SolidBrush AlmondBrush = new SolidBrush(Color.SaddleBrown);

        private int sqwidth = 80;
        private int sqheight = 80;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.ColumnHeader VersionNumber;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Name_test;
        private ColorDialog colorDialog1;
        private TextBox Moves;
        private TextBox bestmovelist;
        private TreeView AnalysisTreeView_AI1;
        private Timer autoplaytimer;
        private Timer timer1;
        private TextBox Scorebox;
        private TextBox Gamesbox;
        private Label label1;
        private Label label2;
        private TextBox gameSummary;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox1;
        private Label label6;
        private TextBox AI1whitebox;
        private TextBox AI1blackbox;
        private TextBox drawbox1;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private TextBox drawbox2;
        private TextBox AI2blackbox;
        private TextBox AI2whitebox;
        private Label label13;
        private Label label14;
        private Label label15;
        private Timer timer2;
        private NumericUpDown AI1_Depth;
        private Label label16;
        private Label label17;
        private NumericUpDown AI2_Depth;
        private Label label18;
        private Label label19;
        private TextBox AI1_ptime;
        private Label label20;
        private TextBox AI2_ptime;
        private Label label21;
        private CheckBox AI1_Q;
        private CheckBox AI2_Q;
        private CheckBox PST_AI2;
        private CheckBox PST_AI1;
        private Label label22;
        private CheckBox SE_AI2;
        private CheckBox SE_AI1;
        private Label label23;
        private CheckBox Ch_AI2;
        private CheckBox Ch_AI1;
        private Label label24;
        private CheckBox Negascout_AI2;
        private CheckBox Negascout_AI1;
        private Label label25;
        private CheckBox IterDeepening_AI2;
        private CheckBox IterDeepening_AI1;
        private Label label26;
        private Label label27;
        private TextBox currentscore;
        private TreeView AnalysisTreeView_AI2;
        private Label label28;
        private TextBox TimePerMove;
        private Label label29;
        private CheckBox KH_AI2;
        private CheckBox KH_AI1;
        private Label label30;
        private CheckBox Randomise;
        private Label label31;
        private NumericUpDown QDepth_AI2;
        private Label label32;
        private NumericUpDown QDepth_AI1;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox ECOmovelist;
        private TextBox ECOMovesTextBox;
        private CheckBox ShowECO;
        private CheckBox FullAnalysis;
        private ComboBox BlackPlayer;
        private Label label33;
        private Label label34;
        private ComboBox WhitePlayer;
        private Button button4;
        private TextBox TurnBox;
        private Label label35;
        private CheckBox checkBox1;
        private Label label36;
        private TextBox textBox2;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private Label label37;
        private Label label38;
        private CheckBox IncrementalBoardEvaluation;
        private CheckBox ShowCutoffs;
        private Button button5;
        private System.ComponentModel.IContainer components;

        public frmMChild_Chess(float scaleF, int squaresize)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            ReadECOCodeFile();
            ShowECOmoves();
            sqwidth = squaresize;
            sqheight = squaresize;

            //
            // TODO: Add any constructor code after InitializeComponent call

        }

        private void ReadECOCodeFile()
        {
            string filenameString = "ECO.txt";
            string line = ""; bool endOfInst = false;            
            string linetrimmed = "";
            string lastcode = "";
            string codestring = "";
            string ECOclass = "";
            string nextcode = "";
            int count = 0;
            try
            {
                SR = File.OpenText(filenameString);
                while (((line = SR.ReadLine()) != null) && (endOfInst != true))
                {
                    linetrimmed = RemoveLeadingWhiteSpace(line);
                    linetrimmed = RemoveTrailingWhiteSpace(linetrimmed);
                    if (linetrimmed.Substring(0, 2) == "--")
                        ECOclass = linetrimmed.Substring(37, 3);
                    if (linetrimmed.Substring(0, 3) == ECOclass)
                        codestring += "|name |" + linetrimmed;
                    if (linetrimmed.Substring(0, 2) == "1.")
                        codestring += "|moves|" + linetrimmed;
                    if ((linetrimmed.Substring(0, 2) != "1.") && (linetrimmed.Substring(0, 3) != ECOclass) && (linetrimmed.Substring(0, 2) != "--"))
                        codestring += " " + linetrimmed;
                }
                SR.Close();

                while (codestring.IndexOf("|name |", 1) != -1)
                {
                    count++;
                    ECOCode code = new ECOCode();
                    nextcode = codestring.Substring(0, codestring.IndexOf("|name |", 1));
                    code.Name = nextcode.Substring(7 + 4, codestring.IndexOf("|moves|", 0) - (7 + 4));
                    code.moves = nextcode.Substring(codestring.IndexOf("|moves|", 0) + 7 + 2);
                    codestring = codestring.Substring(codestring.IndexOf("|name |", 1));
                    ECOCodeList.Add(code);
                }
            }
            catch { }
            //MessageBox.Show("ECO failure, line:" + count.ToString() + " " + lastcode, "ECO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private string RemoveLeadingWhiteSpace(string line)
        {
            for (int i = 0; i < line.Length; i++)
                if ((line.Substring(i, 1) != " ") && (line.Substring(i, 1) != "\t"))
                    return (line.Substring(i));
            return("");            
        }

        private string RemoveTrailingWhiteSpace(string line)
        {
            for (int i = line.Length - 1; i >= 0; i--)
                if ((line.Substring(i, 1) != " ") && (line.Substring(i, 1) != "\t"))
                    return (line.Substring(0,i+1));
            return ("");
        }

        private bool MoveisinECOBook(Point Start, Point Finish)
        {
            string move = ConvertNumtoString(Start.X).ToString() + Start.Y.ToString() + ConvertNumtoString(Finish.X).ToString() + Finish.Y.ToString();
            foreach (ECOCode ecocode in ECOCodeList)
                if (moverecordECO.Length <= ecocode.moves.Length)
                    if (moverecordECO == ecocode.moves.Substring(0, moverecordECO.Length))
                        if (ecocode.moves.Substring(moverecordECO.Length, 4) == move)
                            return(true);//This move is in the book
            return (false);
        }

        private bool ECOmoveAvailable()
        {
            foreach (ECOCode ecocode in ECOCodeList)
                if (moverecordECO.Length <= ecocode.moves.Length)
                    if (moverecordECO == ecocode.moves.Substring(0, moverecordECO.Length))
                        return(true); //There is at least one ECO move
            return (false);
        }

        private void ListECOmoves()
        {
            ECOmovesavailable = ""; ECOmovesavailableCodes = "";
            foreach (ECOCode ecocode in ECOCodeList)
                if (moverecordECO.Length <= ecocode.moves.Length)
                    if (moverecordECO == ecocode.moves.Substring(0, moverecordECO.Length))
                        if (ecocode.moves.Length > moverecordECO.Length + 4 - 1)
                        {
                            ECOmovesavailable += ecocode.moves.Substring(moverecordECO.Length, 4) + " - " + ecocode.Name + "\r\n"; //There is at least one ECO move
                            ECOmovesavailableCodes += ecocode.moves.Substring(moverecordECO.Length, 4) + " ";
                        }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMChild_Chess));
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Name_test = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.Moves = new System.Windows.Forms.TextBox();
            this.bestmovelist = new System.Windows.Forms.TextBox();
            this.AnalysisTreeView_AI1 = new System.Windows.Forms.TreeView();
            this.autoplaytimer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Scorebox = new System.Windows.Forms.TextBox();
            this.Gamesbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gameSummary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AI1whitebox = new System.Windows.Forms.TextBox();
            this.AI1blackbox = new System.Windows.Forms.TextBox();
            this.drawbox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.drawbox2 = new System.Windows.Forms.TextBox();
            this.AI2blackbox = new System.Windows.Forms.TextBox();
            this.AI2whitebox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.AI1_Depth = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.AI2_Depth = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.AI1_ptime = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.AI2_ptime = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.AI1_Q = new System.Windows.Forms.CheckBox();
            this.AI2_Q = new System.Windows.Forms.CheckBox();
            this.PST_AI2 = new System.Windows.Forms.CheckBox();
            this.PST_AI1 = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.SE_AI2 = new System.Windows.Forms.CheckBox();
            this.SE_AI1 = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.Ch_AI2 = new System.Windows.Forms.CheckBox();
            this.Ch_AI1 = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.Negascout_AI2 = new System.Windows.Forms.CheckBox();
            this.Negascout_AI1 = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.IterDeepening_AI2 = new System.Windows.Forms.CheckBox();
            this.IterDeepening_AI1 = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.currentscore = new System.Windows.Forms.TextBox();
            this.AnalysisTreeView_AI2 = new System.Windows.Forms.TreeView();
            this.label28 = new System.Windows.Forms.Label();
            this.TimePerMove = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.KH_AI2 = new System.Windows.Forms.CheckBox();
            this.KH_AI1 = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.Randomise = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.QDepth_AI2 = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.QDepth_AI1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ECOmovelist = new System.Windows.Forms.TextBox();
            this.ECOMovesTextBox = new System.Windows.Forms.TextBox();
            this.ShowECO = new System.Windows.Forms.CheckBox();
            this.FullAnalysis = new System.Windows.Forms.CheckBox();
            this.BlackPlayer = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.WhitePlayer = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.TurnBox = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.IncrementalBoardEvaluation = new System.Windows.Forms.CheckBox();
            this.ShowCutoffs = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AI1_Depth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AI2_Depth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QDepth_AI2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QDepth_AI1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 719);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1362, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusBar1";
            this.statusBar.Visible = false;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            // 
            // Name_test
            // 
            this.Name_test.Text = "name";
            // 
            // Date
            // 
            this.Date.Text = "Date";
            this.Date.Width = 129;
            // 
            // VersionNumber
            // 
            this.VersionNumber.Text = "VersionNumber";
            // 
            // Moves
            // 
            this.Moves.Location = new System.Drawing.Point(974, 34);
            this.Moves.Multiline = true;
            this.Moves.Name = "Moves";
            this.Moves.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Moves.Size = new System.Drawing.Size(106, 749);
            this.Moves.TabIndex = 3;
            // 
            // bestmovelist
            // 
            this.bestmovelist.Location = new System.Drawing.Point(1087, 34);
            this.bestmovelist.Multiline = true;
            this.bestmovelist.Name = "bestmovelist";
            this.bestmovelist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.bestmovelist.Size = new System.Drawing.Size(108, 749);
            this.bestmovelist.TabIndex = 4;
            // 
            // AnalysisTreeView_AI1
            // 
            this.AnalysisTreeView_AI1.Location = new System.Drawing.Point(1203, 34);
            this.AnalysisTreeView_AI1.Name = "AnalysisTreeView_AI1";
            this.AnalysisTreeView_AI1.Size = new System.Drawing.Size(249, 922);
            this.AnalysisTreeView_AI1.TabIndex = 5;
            this.AnalysisTreeView_AI1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AnalysisTreeView_AfterSelect);
            // 
            // autoplaytimer
            // 
            this.autoplaytimer.Enabled = true;
            this.autoplaytimer.Interval = 10000;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // Scorebox
            // 
            this.Scorebox.Location = new System.Drawing.Point(868, 45);
            this.Scorebox.Name = "Scorebox";
            this.Scorebox.Size = new System.Drawing.Size(75, 20);
            this.Scorebox.TabIndex = 6;
            // 
            // Gamesbox
            // 
            this.Gamesbox.Location = new System.Drawing.Point(905, 22);
            this.Gamesbox.Name = "Gamesbox";
            this.Gamesbox.Size = new System.Drawing.Size(37, 20);
            this.Gamesbox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(824, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Score";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(824, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Games Played";
            // 
            // gameSummary
            // 
            this.gameSummary.Location = new System.Drawing.Point(827, 717);
            this.gameSummary.Multiline = true;
            this.gameSummary.Name = "gameSummary";
            this.gameSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gameSummary.Size = new System.Drawing.Size(128, 200);
            this.gameSummary.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(971, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Moves";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1084, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Calculation Times";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1200, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Analysis - AI1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(830, 936);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(113, 20);
            this.textBox1.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(825, 699);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Game Results";
            // 
            // AI1whitebox
            // 
            this.AI1whitebox.Location = new System.Drawing.Point(824, 505);
            this.AI1whitebox.Name = "AI1whitebox";
            this.AI1whitebox.Size = new System.Drawing.Size(32, 20);
            this.AI1whitebox.TabIndex = 16;
            // 
            // AI1blackbox
            // 
            this.AI1blackbox.Location = new System.Drawing.Point(862, 505);
            this.AI1blackbox.Name = "AI1blackbox";
            this.AI1blackbox.Size = new System.Drawing.Size(32, 20);
            this.AI1blackbox.TabIndex = 17;
            // 
            // drawbox1
            // 
            this.drawbox1.Location = new System.Drawing.Point(900, 505);
            this.drawbox1.Name = "drawbox1";
            this.drawbox1.Size = new System.Drawing.Size(32, 20);
            this.drawbox1.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(821, 489);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "White";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(862, 489);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Black";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(902, 489);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Draw";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(902, 541);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Draw";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(862, 541);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Black";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(821, 541);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "White";
            // 
            // drawbox2
            // 
            this.drawbox2.Location = new System.Drawing.Point(900, 557);
            this.drawbox2.Name = "drawbox2";
            this.drawbox2.Size = new System.Drawing.Size(32, 20);
            this.drawbox2.TabIndex = 24;
            // 
            // AI2blackbox
            // 
            this.AI2blackbox.Location = new System.Drawing.Point(862, 557);
            this.AI2blackbox.Name = "AI2blackbox";
            this.AI2blackbox.Size = new System.Drawing.Size(32, 20);
            this.AI2blackbox.TabIndex = 23;
            // 
            // AI2whitebox
            // 
            this.AI2whitebox.Location = new System.Drawing.Point(824, 557);
            this.AI2whitebox.Name = "AI2whitebox";
            this.AI2whitebox.Size = new System.Drawing.Size(32, 20);
            this.AI2whitebox.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(823, 526);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "AI-2 - Black";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(821, 475);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "AI-1 - White";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(827, 920);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(135, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "50 move stalemate counter";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // AI1_Depth
            // 
            this.AI1_Depth.Location = new System.Drawing.Point(855, 92);
            this.AI1_Depth.Name = "AI1_Depth";
            this.AI1_Depth.Size = new System.Drawing.Size(39, 20);
            this.AI1_Depth.TabIndex = 31;
            this.AI1_Depth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(825, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "AI1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(900, 94);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 13);
            this.label17.TabIndex = 34;
            this.label17.Text = "AI2";
            // 
            // AI2_Depth
            // 
            this.AI2_Depth.Location = new System.Drawing.Point(923, 91);
            this.AI2_Depth.Name = "AI2_Depth";
            this.AI2_Depth.Size = new System.Drawing.Size(45, 20);
            this.AI2_Depth.TabIndex = 33;
            this.AI2_Depth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(826, 75);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 13);
            this.label18.TabIndex = 35;
            this.label18.Text = "Search Depth";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(822, 580);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "AI1 - processing time";
            // 
            // AI1_ptime
            // 
            this.AI1_ptime.Location = new System.Drawing.Point(825, 596);
            this.AI1_ptime.Name = "AI1_ptime";
            this.AI1_ptime.Size = new System.Drawing.Size(107, 20);
            this.AI1_ptime.TabIndex = 36;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(821, 619);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(105, 13);
            this.label20.TabIndex = 39;
            this.label20.Text = "AI2 - processing time";
            // 
            // AI2_ptime
            // 
            this.AI2_ptime.Location = new System.Drawing.Point(825, 635);
            this.AI2_ptime.Name = "AI2_ptime";
            this.AI2_ptime.Size = new System.Drawing.Size(107, 20);
            this.AI2_ptime.TabIndex = 38;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(826, 114);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(119, 13);
            this.label21.TabIndex = 44;
            this.label21.Text = "Quiesce (Captures only)";
            // 
            // AI1_Q
            // 
            this.AI1_Q.AutoSize = true;
            this.AI1_Q.Checked = true;
            this.AI1_Q.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AI1_Q.Location = new System.Drawing.Point(827, 130);
            this.AI1_Q.Name = "AI1_Q";
            this.AI1_Q.Size = new System.Drawing.Size(42, 17);
            this.AI1_Q.TabIndex = 45;
            this.AI1_Q.Text = "AI1";
            this.AI1_Q.UseVisualStyleBackColor = true;
            // 
            // AI2_Q
            // 
            this.AI2_Q.AutoSize = true;
            this.AI2_Q.Checked = true;
            this.AI2_Q.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AI2_Q.Location = new System.Drawing.Point(875, 130);
            this.AI2_Q.Name = "AI2_Q";
            this.AI2_Q.Size = new System.Drawing.Size(42, 17);
            this.AI2_Q.TabIndex = 46;
            this.AI2_Q.Text = "AI2";
            this.AI2_Q.UseVisualStyleBackColor = true;
            // 
            // PST_AI2
            // 
            this.PST_AI2.AutoSize = true;
            this.PST_AI2.Checked = true;
            this.PST_AI2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PST_AI2.Location = new System.Drawing.Point(873, 200);
            this.PST_AI2.Name = "PST_AI2";
            this.PST_AI2.Size = new System.Drawing.Size(42, 17);
            this.PST_AI2.TabIndex = 49;
            this.PST_AI2.Text = "AI2";
            this.PST_AI2.UseVisualStyleBackColor = true;
            // 
            // PST_AI1
            // 
            this.PST_AI1.AutoSize = true;
            this.PST_AI1.Checked = true;
            this.PST_AI1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PST_AI1.Location = new System.Drawing.Point(825, 200);
            this.PST_AI1.Name = "PST_AI1";
            this.PST_AI1.Size = new System.Drawing.Size(42, 17);
            this.PST_AI1.TabIndex = 48;
            this.PST_AI1.Text = "AI1";
            this.PST_AI1.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(821, 184);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(128, 13);
            this.label22.TabIndex = 47;
            this.label22.Text = "Use Piece Square Tables";
            // 
            // SE_AI2
            // 
            this.SE_AI2.AutoSize = true;
            this.SE_AI2.Checked = true;
            this.SE_AI2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SE_AI2.Location = new System.Drawing.Point(872, 236);
            this.SE_AI2.Name = "SE_AI2";
            this.SE_AI2.Size = new System.Drawing.Size(42, 17);
            this.SE_AI2.TabIndex = 52;
            this.SE_AI2.Text = "AI2";
            this.SE_AI2.UseVisualStyleBackColor = true;
            // 
            // SE_AI1
            // 
            this.SE_AI1.AutoSize = true;
            this.SE_AI1.Checked = true;
            this.SE_AI1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SE_AI1.Location = new System.Drawing.Point(826, 236);
            this.SE_AI1.Name = "SE_AI1";
            this.SE_AI1.Size = new System.Drawing.Size(42, 17);
            this.SE_AI1.TabIndex = 51;
            this.SE_AI1.Text = "AI1";
            this.SE_AI1.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(821, 220);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(143, 13);
            this.label23.TabIndex = 50;
            this.label23.Text = "End game search extensions";
            // 
            // Ch_AI2
            // 
            this.Ch_AI2.AutoSize = true;
            this.Ch_AI2.Checked = true;
            this.Ch_AI2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ch_AI2.Location = new System.Drawing.Point(872, 272);
            this.Ch_AI2.Name = "Ch_AI2";
            this.Ch_AI2.Size = new System.Drawing.Size(42, 17);
            this.Ch_AI2.TabIndex = 55;
            this.Ch_AI2.Text = "AI2";
            this.Ch_AI2.UseVisualStyleBackColor = true;
            // 
            // Ch_AI1
            // 
            this.Ch_AI1.AutoSize = true;
            this.Ch_AI1.Checked = true;
            this.Ch_AI1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ch_AI1.Location = new System.Drawing.Point(824, 272);
            this.Ch_AI1.Name = "Ch_AI1";
            this.Ch_AI1.Size = new System.Drawing.Size(42, 17);
            this.Ch_AI1.TabIndex = 54;
            this.Ch_AI1.Text = "AI1";
            this.Ch_AI1.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(821, 256);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(129, 13);
            this.label24.TabIndex = 53;
            this.label24.Text = "Check Search Extensions";
            // 
            // Negascout_AI2
            // 
            this.Negascout_AI2.AutoSize = true;
            this.Negascout_AI2.Checked = true;
            this.Negascout_AI2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Negascout_AI2.Location = new System.Drawing.Point(872, 308);
            this.Negascout_AI2.Name = "Negascout_AI2";
            this.Negascout_AI2.Size = new System.Drawing.Size(42, 17);
            this.Negascout_AI2.TabIndex = 58;
            this.Negascout_AI2.Text = "AI2";
            this.Negascout_AI2.UseVisualStyleBackColor = true;
            // 
            // Negascout_AI1
            // 
            this.Negascout_AI1.AutoSize = true;
            this.Negascout_AI1.Checked = true;
            this.Negascout_AI1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Negascout_AI1.Location = new System.Drawing.Point(824, 308);
            this.Negascout_AI1.Name = "Negascout_AI1";
            this.Negascout_AI1.Size = new System.Drawing.Size(42, 17);
            this.Negascout_AI1.TabIndex = 57;
            this.Negascout_AI1.Text = "AI1";
            this.Negascout_AI1.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(821, 292);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(126, 13);
            this.label25.TabIndex = 56;
            this.label25.Text = "Use Negascout algorithm";
            // 
            // IterDeepening_AI2
            // 
            this.IterDeepening_AI2.AutoSize = true;
            this.IterDeepening_AI2.Checked = true;
            this.IterDeepening_AI2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IterDeepening_AI2.Location = new System.Drawing.Point(872, 344);
            this.IterDeepening_AI2.Name = "IterDeepening_AI2";
            this.IterDeepening_AI2.Size = new System.Drawing.Size(42, 17);
            this.IterDeepening_AI2.TabIndex = 61;
            this.IterDeepening_AI2.Text = "AI2";
            this.IterDeepening_AI2.UseVisualStyleBackColor = true;
            // 
            // IterDeepening_AI1
            // 
            this.IterDeepening_AI1.AutoSize = true;
            this.IterDeepening_AI1.Checked = true;
            this.IterDeepening_AI1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IterDeepening_AI1.Location = new System.Drawing.Point(824, 344);
            this.IterDeepening_AI1.Name = "IterDeepening_AI1";
            this.IterDeepening_AI1.Size = new System.Drawing.Size(42, 17);
            this.IterDeepening_AI1.TabIndex = 60;
            this.IterDeepening_AI1.Text = "AI1";
            this.IterDeepening_AI1.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(821, 328);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(119, 13);
            this.label26.TabIndex = 59;
            this.label26.Text = "Use iterative deepening";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(824, 658);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 13);
            this.label27.TabIndex = 63;
            this.label27.Text = "Score";
            // 
            // currentscore
            // 
            this.currentscore.Location = new System.Drawing.Point(826, 677);
            this.currentscore.Name = "currentscore";
            this.currentscore.Size = new System.Drawing.Size(75, 20);
            this.currentscore.TabIndex = 62;
            // 
            // AnalysisTreeView_AI2
            // 
            this.AnalysisTreeView_AI2.Location = new System.Drawing.Point(1458, 34);
            this.AnalysisTreeView_AI2.Name = "AnalysisTreeView_AI2";
            this.AnalysisTreeView_AI2.Size = new System.Drawing.Size(382, 922);
            this.AnalysisTreeView_AI2.TabIndex = 64;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(1455, 17);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 13);
            this.label28.TabIndex = 65;
            this.label28.Text = "Analysis - AI2";
            // 
            // TimePerMove
            // 
            this.TimePerMove.Location = new System.Drawing.Point(828, 977);
            this.TimePerMove.Name = "TimePerMove";
            this.TimePerMove.Size = new System.Drawing.Size(113, 20);
            this.TimePerMove.TabIndex = 66;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(825, 959);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(116, 13);
            this.label29.TabIndex = 67;
            this.label29.Text = "Average time per move";
            // 
            // KH_AI2
            // 
            this.KH_AI2.AutoSize = true;
            this.KH_AI2.Checked = true;
            this.KH_AI2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KH_AI2.Location = new System.Drawing.Point(873, 380);
            this.KH_AI2.Name = "KH_AI2";
            this.KH_AI2.Size = new System.Drawing.Size(42, 17);
            this.KH_AI2.TabIndex = 70;
            this.KH_AI2.Text = "AI2";
            this.KH_AI2.UseVisualStyleBackColor = true;
            // 
            // KH_AI1
            // 
            this.KH_AI1.AutoSize = true;
            this.KH_AI1.Checked = true;
            this.KH_AI1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KH_AI1.Location = new System.Drawing.Point(825, 380);
            this.KH_AI1.Name = "KH_AI1";
            this.KH_AI1.Size = new System.Drawing.Size(42, 17);
            this.KH_AI1.TabIndex = 69;
            this.KH_AI1.Text = "AI1";
            this.KH_AI1.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(822, 364);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(100, 13);
            this.label30.TabIndex = 68;
            this.label30.Text = "Use Killer Heuristics";
            // 
            // Randomise
            // 
            this.Randomise.AutoSize = true;
            this.Randomise.Location = new System.Drawing.Point(824, 403);
            this.Randomise.Name = "Randomise";
            this.Randomise.Size = new System.Drawing.Size(79, 17);
            this.Randomise.TabIndex = 71;
            this.Randomise.Text = "Randomise";
            this.Randomise.UseVisualStyleBackColor = true;
            this.Randomise.CheckedChanged += new System.EventHandler(this.Randomise_CheckedChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(900, 154);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(23, 13);
            this.label31.TabIndex = 75;
            this.label31.Text = "AI2";
            // 
            // QDepth_AI2
            // 
            this.QDepth_AI2.Location = new System.Drawing.Point(923, 151);
            this.QDepth_AI2.Name = "QDepth_AI2";
            this.QDepth_AI2.Size = new System.Drawing.Size(45, 20);
            this.QDepth_AI2.TabIndex = 74;
            this.QDepth_AI2.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(825, 154);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(23, 13);
            this.label32.TabIndex = 73;
            this.label32.Text = "AI1";
            // 
            // QDepth_AI1
            // 
            this.QDepth_AI1.Location = new System.Drawing.Point(855, 152);
            this.QDepth_AI1.Name = "QDepth_AI1";
            this.QDepth_AI1.Size = new System.Drawing.Size(39, 20);
            this.QDepth_AI1.TabIndex = 72;
            this.QDepth_AI1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(731, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 76;
            this.button1.Text = "Take Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(736, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 77;
            this.button2.Text = "AI1 vs AI2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(735, 143);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 23);
            this.button3.TabIndex = 78;
            this.button3.Text = "Rotate Board";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ECOmovelist
            // 
            this.ECOmovelist.Location = new System.Drawing.Point(543, 959);
            this.ECOmovelist.Multiline = true;
            this.ECOmovelist.Name = "ECOmovelist";
            this.ECOmovelist.Size = new System.Drawing.Size(278, 38);
            this.ECOmovelist.TabIndex = 79;
            // 
            // ECOMovesTextBox
            // 
            this.ECOMovesTextBox.Location = new System.Drawing.Point(974, 789);
            this.ECOMovesTextBox.Multiline = true;
            this.ECOMovesTextBox.Name = "ECOMovesTextBox";
            this.ECOMovesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ECOMovesTextBox.Size = new System.Drawing.Size(221, 167);
            this.ECOMovesTextBox.TabIndex = 80;
            // 
            // ShowECO
            // 
            this.ShowECO.AutoSize = true;
            this.ShowECO.Checked = true;
            this.ShowECO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowECO.Location = new System.Drawing.Point(900, 403);
            this.ShowECO.Name = "ShowECO";
            this.ShowECO.Size = new System.Drawing.Size(81, 17);
            this.ShowECO.TabIndex = 81;
            this.ShowECO.Text = "ECO Codes";
            this.ShowECO.UseVisualStyleBackColor = true;
            // 
            // FullAnalysis
            // 
            this.FullAnalysis.AutoSize = true;
            this.FullAnalysis.Checked = true;
            this.FullAnalysis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FullAnalysis.Location = new System.Drawing.Point(824, 449);
            this.FullAnalysis.Name = "FullAnalysis";
            this.FullAnalysis.Size = new System.Drawing.Size(109, 17);
            this.FullAnalysis.TabIndex = 82;
            this.FullAnalysis.Text = "Show full analysis";
            this.FullAnalysis.UseVisualStyleBackColor = true;
            this.FullAnalysis.CheckedChanged += new System.EventHandler(this.FullAnalysis_CheckedChanged);
            // 
            // BlackPlayer
            // 
            this.BlackPlayer.FormattingEnabled = true;
            this.BlackPlayer.Items.AddRange(new object[] {
            "Human",
            "AI"});
            this.BlackPlayer.Location = new System.Drawing.Point(759, 116);
            this.BlackPlayer.Name = "BlackPlayer";
            this.BlackPlayer.Size = new System.Drawing.Size(62, 21);
            this.BlackPlayer.TabIndex = 83;
            this.BlackPlayer.Text = "AI";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(720, 116);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(34, 13);
            this.label33.TabIndex = 84;
            this.label33.Text = "Black";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(720, 98);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(35, 13);
            this.label34.TabIndex = 86;
            this.label34.Text = "White";
            // 
            // WhitePlayer
            // 
            this.WhitePlayer.FormattingEnabled = true;
            this.WhitePlayer.Items.AddRange(new object[] {
            "Human",
            "AI"});
            this.WhitePlayer.Location = new System.Drawing.Point(759, 94);
            this.WhitePlayer.Name = "WhitePlayer";
            this.WhitePlayer.Size = new System.Drawing.Size(62, 21);
            this.WhitePlayer.TabIndex = 85;
            this.WhitePlayer.Text = "Human";
            this.WhitePlayer.TextChanged += new System.EventHandler(this.WhitePlayer_TextChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(736, 65);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(46, 23);
            this.button4.TabIndex = 87;
            this.button4.Text = "Start";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.TextChanged += new System.EventHandler(this.button4_TextChanged);
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TurnBox
            // 
            this.TurnBox.Location = new System.Drawing.Point(774, 217);
            this.TurnBox.Name = "TurnBox";
            this.TurnBox.Size = new System.Drawing.Size(38, 20);
            this.TurnBox.TabIndex = 88;
            this.TurnBox.Text = "White";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(736, 220);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(32, 13);
            this.label35.TabIndex = 89;
            this.label35.Text = "Turn:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(753, 170);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 90;
            this.checkBox1.Text = "Optimise";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(947, 962);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(65, 13);
            this.label36.TabIndex = 92;
            this.label36.Text = "Loop Count:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(950, 977);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(72, 20);
            this.textBox2.TabIndex = 91;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(1147, 980);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(42, 17);
            this.checkBox2.TabIndex = 95;
            this.checkBox2.Text = "AI2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(1099, 980);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(42, 17);
            this.checkBox3.TabIndex = 94;
            this.checkBox3.Text = "AI1";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(1037, 980);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(56, 13);
            this.label37.TabIndex = 93;
            this.label37.Text = "Pawn Blitz";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(1037, 959);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(58, 13);
            this.label38.TabIndex = 96;
            this.label38.Text = "Handicaps";
            // 
            // IncrementalBoardEvaluation
            // 
            this.IncrementalBoardEvaluation.AutoSize = true;
            this.IncrementalBoardEvaluation.Checked = true;
            this.IncrementalBoardEvaluation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncrementalBoardEvaluation.Location = new System.Drawing.Point(1195, 976);
            this.IncrementalBoardEvaluation.Name = "IncrementalBoardEvaluation";
            this.IncrementalBoardEvaluation.Size = new System.Drawing.Size(164, 17);
            this.IncrementalBoardEvaluation.TabIndex = 97;
            this.IncrementalBoardEvaluation.Text = "Incremental board Evaluation";
            this.IncrementalBoardEvaluation.UseVisualStyleBackColor = true;
            // 
            // ShowCutoffs
            // 
            this.ShowCutoffs.AutoSize = true;
            this.ShowCutoffs.Checked = true;
            this.ShowCutoffs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowCutoffs.Location = new System.Drawing.Point(824, 426);
            this.ShowCutoffs.Name = "ShowCutoffs";
            this.ShowCutoffs.Size = new System.Drawing.Size(89, 17);
            this.ShowCutoffs.TabIndex = 98;
            this.ShowCutoffs.Text = "Show Cutoffs";
            this.ShowCutoffs.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(737, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 23);
            this.button5.TabIndex = 99;
            this.button5.Text = "AI1 vs AI2";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // frmMChild_Chess
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.ShowCutoffs);
            this.Controls.Add(this.IncrementalBoardEvaluation);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.TurnBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.WhitePlayer);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.BlackPlayer);
            this.Controls.Add(this.FullAnalysis);
            this.Controls.Add(this.ShowECO);
            this.Controls.Add(this.ECOMovesTextBox);
            this.Controls.Add(this.ECOmovelist);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.QDepth_AI2);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.QDepth_AI1);
            this.Controls.Add(this.Randomise);
            this.Controls.Add(this.KH_AI2);
            this.Controls.Add(this.KH_AI1);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.TimePerMove);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.AnalysisTreeView_AI2);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.currentscore);
            this.Controls.Add(this.IterDeepening_AI2);
            this.Controls.Add(this.IterDeepening_AI1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.Negascout_AI2);
            this.Controls.Add(this.Negascout_AI1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.Ch_AI2);
            this.Controls.Add(this.Ch_AI1);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.SE_AI2);
            this.Controls.Add(this.SE_AI1);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.PST_AI2);
            this.Controls.Add(this.PST_AI1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.AI2_Q);
            this.Controls.Add(this.AI1_Q);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.AI2_ptime);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.AI1_ptime);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.AI2_Depth);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.AI1_Depth);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.drawbox2);
            this.Controls.Add(this.AI2blackbox);
            this.Controls.Add(this.AI2whitebox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.drawbox1);
            this.Controls.Add(this.AI1blackbox);
            this.Controls.Add(this.AI1whitebox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gameSummary);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Gamesbox);
            this.Controls.Add(this.Scorebox);
            this.Controls.Add(this.AnalysisTreeView_AI1);
            this.Controls.Add(this.bestmovelist);
            this.Controls.Add(this.Moves);
            this.Controls.Add(this.statusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMChild_Chess";
            this.Text = "Chess";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMChild_Chess_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMChild_Chess_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMChild_Chess_MouseUp);
            this.Resize += new System.EventHandler(this.frmMChild_Chess_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.AI1_Depth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AI2_Depth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QDepth_AI2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QDepth_AI1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void DrawBoard()
        {
            for (int i = 1; i < 9; i++)
                for (int j = 1; j < 9; j++)
                {
                    if ((i + j) == 2 * ((int)Math.Floor((double)((i + j) / 2))))
                        eGfx.FillRectangle(WhiteBrush, i * sqwidth, j * sqheight, sqwidth, sqheight);
                    else
                        eGfx.FillRectangle(BisqueBrush, i * sqwidth, j * sqheight, sqwidth, sqheight);
                }
            eGfx.FillRectangle(AlmondBrush, sqwidth / 2, sqheight / 2, (int)9.5 * sqwidth, sqheight / 2);
            eGfx.FillRectangle(AlmondBrush, sqwidth / 2, sqheight / 2, sqwidth / 2, (int)9.5 * sqheight);//
            eGfx.FillRectangle(AlmondBrush, 9 * sqwidth, sqheight / 2, sqwidth / 2, 9 * sqheight);
            eGfx.FillRectangle(AlmondBrush, sqwidth / 2, 9 * sqheight, 9 * sqwidth, sqheight / 2);
        }

        private void frmMChild_Chess_Load(object sender, EventArgs e)
        {

            // Magic code that allows fast screen drawing
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();


            System.Drawing.Bitmap WhiteRookbmp = Logic_Navigator.Properties.Resources.WhiteRook;            
            System.Drawing.Bitmap WhitePawnbmp = Logic_Navigator.Properties.Resources.WhitePawn;
            System.Drawing.Bitmap WhiteBishopbmp = Logic_Navigator.Properties.Resources.WhiteBishop;
            System.Drawing.Bitmap WhiteKingbmp = Logic_Navigator.Properties.Resources.WhiteKing;
            System.Drawing.Bitmap WhiteQueenbmp = Logic_Navigator.Properties.Resources.WhiteQueen;
            System.Drawing.Bitmap WhiteKnightbmp = Logic_Navigator.Properties.Resources.WhiteKnight;

            System.Drawing.Bitmap BlackRookbmp = Logic_Navigator.Properties.Resources.BlackRook;
            System.Drawing.Bitmap BlackPawnbmp = Logic_Navigator.Properties.Resources.BlackPawn;
            System.Drawing.Bitmap BlackBishopbmp = Logic_Navigator.Properties.Resources.BlackBishop;
            System.Drawing.Bitmap BlackKingbmp = Logic_Navigator.Properties.Resources.BlackKing;
            System.Drawing.Bitmap BlackQueenbmp = Logic_Navigator.Properties.Resources.BlackQueen;
            System.Drawing.Bitmap BlackKnightbmp = Logic_Navigator.Properties.Resources.BlackKnight;
            
            WhitePawn = (Image) WhitePawnbmp;
            WhiteRook = (Image) WhiteRookbmp;
            WhiteBishop = (Image) WhiteBishopbmp;
            WhiteKnight = (Image) WhiteKnightbmp;
            WhiteKing = (Image) WhiteKingbmp;
            WhiteQueen = (Image) WhiteQueenbmp;

            BlackPawn = (Image) BlackPawnbmp;
            BlackRook = (Image) BlackRookbmp;
            BlackBishop = (Image) BlackBishopbmp;
            BlackKnight = (Image) BlackKnightbmp;
            BlackKing = (Image) BlackKingbmp;
            BlackQueen = (Image) BlackQueenbmp;

            attr.SetColorKey(Color.White, Color.White);
            drawFormat.Alignment = StringAlignment.Center;
            gamestate = InitialiseGameState();
        }

        private void frmMChild_Chess_Paint(object sender, PaintEventArgs e)
        {
            string progress = "";
            try
            {
                eGfx = e.Graphics;
                DrawBoard();
                //Image Piece = null;
                RectangleF drawRect = new RectangleF(0, 0, 800, 100);
                //eGfx.DrawString(debuginfo, drawFont, RedBrush, drawRect, drawFormat);
                drawRect = new RectangleF(1000, 0, movenumber * 100, 500);
                //eGfx.DrawString(DateTime.Now.ToString(), drawFont, RedBrush, drawRect, drawFormat);
                progress = "Drawboard";
                if (ShowCutoffs.Checked)
                    ShowBestMoves();
                progress = "BestMoves";
                ShowLastMove();
                progress = "LastMove";
                if(!thinking) 
                    Drawpieces(ChessPositions);
                else
                    Drawpieces(ChessPositionsbeingconsidered);
                progress = "Drawpieces";
                DrawTakenPieces();
                DrawScore();
                progress = "DrawScore";
                if(View == "White")
                    for (int i = 0; i < 8; i++)                
                    {
                        Rectangle rec = new Rectangle((i + 1) * sqwidth, (9) * sqheight, sqwidth, sqheight);
                        eGfx.DrawString(ConvertNumtoString(i + 1), drawFont, BlueBrush, rec, drawFormat);
                        Rectangle rec1 = new Rectangle((int)(0.25 * sqwidth), (int)((0.5 + (8 - i)) * sqheight), sqwidth, sqheight);
                        eGfx.DrawString((i + 1).ToString(), drawFont, BlueBrush, rec1, drawFormat);
                    }
                else
                    for (int i = 0; i < 8; i++)   
                    {
                        Rectangle rec = new Rectangle((i + 1) * sqwidth, (9) * sqheight, sqwidth, sqheight);
                        eGfx.DrawString(ConvertNumtoString(8 - i), drawFont, BlueBrush, rec, drawFormat);
                        Rectangle rec1 = new Rectangle((int)(0.25 * sqwidth), (int)((0.5 + (8 - i)) * sqheight), sqwidth, sqheight);
                        eGfx.DrawString((8 - i).ToString(), drawFont, BlueBrush, rec1, drawFormat);
                    }
                Moves.Text = moverecord + "\r\n" + moverecordECO;
                progress = "moverecord";
                if(ShowECO.Checked) ShowECOlines();
                progress = "ECOlines";
                bestmovelist.Text = bestmoves;
                Rectangle rec3 = new Rectangle(12 * sqwidth, sqheight / 3, 3 * sqwidth, 10 * sqheight);
                //eGfx.DrawString(StateOfPlaytoString(), drawFont, BlueBrush, rec3, drawFormat);
                if (piecegrabbed)
                {
                    Rectangle recdragged = new Rectangle(mouseposition.X - offset.X, mouseposition.Y - offset.Y, sqwidth, sqheight);
                    eGfx.DrawImage(GetImage(draggedpiece), recdragged, 0, 0, 128, 128, GraphicsUnit.Pixel, attr);
                }
            }
            catch { MessageBox.Show("Paint: " + progress, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void ShowECOlines()
        {   
            Point fromsq = new Point(0, 0);
            Point tosq = new Point(0, 0);
            Pen ECOPen = new Pen(Color.Green);
            string leftECO = ECOmovesavailableCodes;
            while(leftECO.Length > 5)
            {
                fromsq.X = (int) leftECO[0] - 97;//97 - ansi "a"
                fromsq.Y = (int) leftECO[1] - 48;//48 - ansi "1"              
                tosq.X = (int) leftECO[2] - 97;
                tosq.Y = (int) leftECO[3] - 48;
                
                if(View == "White")
                { 
                    eGfx.DrawLine(ECOPen, (fromsq.X + 1) * sqwidth + (sqwidth / 2), (1 + 8 - fromsq.Y) * sqheight + (sqheight / 2), 
                                                (tosq.X + 1) * sqwidth + (sqwidth / 2), (1 + 8 - tosq.Y) * sqheight + (sqheight / 2));
                    eGfx.DrawEllipse(ECOPen, -5 + (fromsq.X + 1) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - fromsq.Y) * sqheight + (sqheight / 2), 10, 10);
                    eGfx.DrawEllipse(ECOPen, -5 + (tosq.X + 1) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - tosq.Y) * sqheight + (sqheight / 2), 10, 10);
                }
                if (View == "Black")
                {
                    eGfx.DrawLine(ECOPen, 1 + 8 - (fromsq.X + 1) * sqwidth + (sqwidth / 2), (fromsq.Y) * sqheight + (sqheight / 2),
                                          1 + 8 - (tosq.X + 1) * sqwidth + (sqwidth / 2), (tosq.Y) * sqheight + (sqheight / 2));
                    eGfx.DrawEllipse(ECOPen, -5 + 1 + 8 - (fromsq.X + 1) * sqwidth + (sqwidth / 2), -5 + (fromsq.Y) * sqheight + (sqheight / 2), 10, 10);
                    eGfx.DrawEllipse(ECOPen, -5 + 1 + 8 - (tosq.X + 1) * sqwidth + (sqwidth / 2), -5 + (tosq.Y) * sqheight + (sqheight / 2), 10, 10);
                }

                if(leftECO.Length > 5)
                    leftECO = leftECO.Substring(5);
            }                       
        }
        
        //string move = tn2.Nodes[i].Text;
        private void ShowBestMoves()
        {

            Pen OptionsPen = new Pen(Color.PaleGreen);
            OptionsPen.Width++;
            SolidBrush ValueBrush = new SolidBrush(Color.HotPink);
            PointF drawspot = new PointF(0.0f, 0.0f);

            OptionsPen.Width = 2;

            TreeView AnalysisTreeView;
            if (currentAI == AI1)
                AnalysisTreeView = AnalysisTreeView_AI1;
            else AnalysisTreeView = AnalysisTreeView_AI2;
            foreach (TreeNode topnode in AnalysisTreeView.Nodes)
                foreach (TreeNode tn1 in topnode.Nodes)
                    if (tn1.ToString().IndexOf("cutoff") != -1)
                    {
                        try { 
                        int startx = Convert.ToInt32(tn1.Text.Substring(12, 1));
                        int starty = Convert.ToInt32(tn1.Text.Substring(14, 1));
                        int finishx = Convert.ToInt32(tn1.Text.Substring(18, 1));
                        int finishy = Convert.ToInt32(tn1.Text.Substring(20, 1));
                        int value = Convert.ToInt32(tn1.Text.Substring(tn1.Text.IndexOf("Value") + 6, tn1.Text.Length - (tn1.Text.IndexOf("Value") + 7))) - score;
                    
                        //cutoff[1], (1,7),(1,5), (Alpha:-1, Beta:100000, Value:-1)

                        if (View == "White")
                        {
                            if (turn == Black)
                            {
                                drawspot.X = -5 + (finishx) * sqwidth + (sqwidth / 2) + sqwidth/4;
                                drawspot.Y = -5 + (1 + 8 - finishy) * sqheight + (sqheight / 2);
                                eGfx.DrawLine(OptionsPen, (startx) * sqwidth + (sqwidth / 2), (1 + 8 - starty) * sqheight + (sqheight / 2),
                                                              (finishx) * sqwidth + (sqwidth / 2), (1 + 8 - finishy) * sqheight + (sqheight / 2));
                                eGfx.DrawEllipse(OptionsPen, -5 + (startx) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - starty) * sqheight + (sqheight / 2), 10, 10);
                                eGfx.DrawEllipse(OptionsPen, -5 + (finishx) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - finishy) * sqheight + (sqheight / 2), 10, 10);
                            }
                            if (turn == White)
                            {
                                drawspot.X = -5 + (finishx) * sqwidth + (sqwidth / 2) + sqwidth / 4;
                                drawspot.Y = -5 + (1 + 8 - finishx) * sqheight + (sqheight / 2);
                                eGfx.DrawLine(OptionsPen, (startx) * sqwidth + (sqwidth / 2), (1 + 8 - starty) * sqheight + (sqheight / 2),
                                                             (finishx) * sqwidth + (sqwidth / 2), (1 + 8 - finishy) * sqheight + (sqheight / 2));
                                eGfx.DrawEllipse(OptionsPen, -5 + (startx) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - starty) * sqheight + (sqheight / 2), 10, 10);
                                eGfx.DrawEllipse(OptionsPen, -5 + (finishx) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - finishy) * sqheight + (sqheight / 2), 10, 10);
                            }
                        }
                        else
                        {
                            if (turn == Black)
                            {
                                drawspot.X = -5 + (finishx) * sqwidth + (sqwidth / 2) + sqwidth / 4;
                                drawspot.Y = -5 + (1 + 8 - finishy) * sqheight + (sqheight / 2);
                                eGfx.DrawLine(OptionsPen, (1 + 8 - startx) * sqwidth + (sqwidth / 2), (starty) * sqheight + (sqheight / 2),
                                                           (1 + 8 - finishx) * sqwidth + (sqwidth / 2), (finishy) * sqheight + (sqheight / 2));
                                eGfx.DrawEllipse(OptionsPen, -5 + (1 + 8 - startx) * sqwidth + (sqwidth / 2), -5 + (starty) * sqheight + (sqheight / 2), 10, 10);
                                eGfx.DrawEllipse(OptionsPen, -5 + (1 + 8 - finishx) * sqwidth + (sqwidth / 2), -5 + (finishy) * sqheight + (sqheight / 2), 10, 10);
                            }
                            if (turn == White)
                            {

                                drawspot.X = -5 + (1 + 8 - finishx) * sqwidth + (sqwidth / 2) + sqwidth / 4;
                                drawspot.Y = -5 + (finishy) * sqheight + (sqheight / 2);
                                eGfx.DrawLine(OptionsPen, (1 + 8 - startx) * sqwidth + (sqwidth / 2), (starty) * sqheight + (sqheight / 2),
                                                        (1 + 8 - finishx) * sqwidth + (sqwidth / 2), (finishy) * sqheight + (sqheight / 2));
                                eGfx.DrawEllipse(OptionsPen, -5 + (1 + 8 - startx) * sqwidth + (sqwidth / 2), -5 + (starty) * sqheight + (sqheight / 2), 10, 10);
                                eGfx.DrawEllipse(OptionsPen, -5 + (1 + 8 - finishx) * sqwidth + (sqwidth / 2), -5 + (finishy) * sqheight + (sqheight / 2), 10, 10);
                            }
                        }
                        eGfx.DrawString(value.ToString(), drawFont, ValueBrush, drawspot);
                        OptionsPen.Width++;
                        }
                        catch (FormatException) {}
                    }
        }


        private void ShowLastMove()
        {            
            Pen LastMovePen = new Pen(Color.Green);
            LastMovePen.Width = 2;
            //LastMovePen.PenType =
            if (View == "White")
            {
                if (turn == Black)
                {
                    eGfx.DrawLine(LastMovePen, (blacklastmove_start.X) * sqwidth + (sqwidth / 2), (1 + 8 - blacklastmove_start.Y) * sqheight + (sqheight / 2),
                                                  (blacklastmove_finish.X) * sqwidth + (sqwidth / 2), (1 + 8 - blacklastmove_finish.Y) * sqheight + (sqheight / 2));
                    eGfx.DrawEllipse(LastMovePen, -5 + (blacklastmove_start.X) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - blacklastmove_start.Y) * sqheight + (sqheight / 2), 10, 10);                    
                    eGfx.DrawEllipse(LastMovePen, -5 + (blacklastmove_finish.X) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - blacklastmove_finish.Y) * sqheight + (sqheight / 2), 10, 10);
                }
                if (turn == White)
                {
                    eGfx.DrawLine(LastMovePen, (whitelastmove_start.X) * sqwidth + (sqwidth / 2), (1 + 8 - whitelastmove_start.Y) * sqheight + (sqheight / 2),
                                                 (whitelastmove_finish.X) * sqwidth + (sqwidth / 2), (1 + 8 - whitelastmove_finish.Y) * sqheight + (sqheight / 2));
                    eGfx.DrawEllipse(LastMovePen, -5 + (whitelastmove_start.X) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - whitelastmove_start.Y) * sqheight + (sqheight / 2), 10, 10);
                    eGfx.DrawEllipse(LastMovePen, -5 + (whitelastmove_finish.X) * sqwidth + (sqwidth / 2), -5 + (1 + 8 - whitelastmove_finish.Y) * sqheight + (sqheight / 2), 10, 10);
                }
            }
            else
            {
                if (turn == Black)
                {
                    eGfx.DrawLine(LastMovePen, (1 + 8 - blacklastmove_start.X) * sqwidth + (sqwidth / 2), (blacklastmove_start.Y) * sqheight + (sqheight / 2),
                                               (1 + 8 - blacklastmove_finish.X) * sqwidth + (sqwidth / 2), (blacklastmove_finish.Y) * sqheight + (sqheight / 2));                    
                    eGfx.DrawEllipse(LastMovePen, -5 + (1 + 8 - blacklastmove_start.X) * sqwidth + (sqwidth / 2), -5 + (blacklastmove_start.Y) * sqheight + (sqheight / 2), 10, 10);                    
                    eGfx.DrawEllipse(LastMovePen, -5 + (1 + 8 - blacklastmove_finish.X) * sqwidth + (sqwidth / 2), -5 + (blacklastmove_finish.Y) * sqheight + (sqheight / 2), 10, 10);
                }
                if (turn == White)
                {
                    eGfx.DrawLine(LastMovePen, (1 + 8 - whitelastmove_start.X) * sqwidth + (sqwidth / 2), (whitelastmove_start.Y) * sqheight + (sqheight / 2),
                                            (1 + 8 - whitelastmove_finish.X) * sqwidth + (sqwidth / 2), (whitelastmove_finish.Y) * sqheight + (sqheight / 2));
                    eGfx.DrawEllipse(LastMovePen, -5 + (1 + 8 - whitelastmove_start.X) * sqwidth + (sqwidth / 2), -5 + (whitelastmove_start.Y) * sqheight + (sqheight / 2), 10, 10);
                    eGfx.DrawEllipse(LastMovePen, -5 + (1 + 8 - whitelastmove_finish.X) * sqwidth + (sqwidth / 2), -5 + (whitelastmove_finish.Y) * sqheight + (sqheight / 2), 10, 10);
                }
            }
        }

        private void Drawpieces(int[,] Board)
        {
            Image Piece = null;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Piece = null;
                    Piece = GetImage((int)Board[i, j]);
                    if ((j + 1 == clicked.X) && (i + 1 == clicked.Y) && piecegrabbed)
                        Piece = null;
                    if (Piece != null)
                    {
                        Rectangle rec = new Rectangle((j + 1) * sqwidth, (8 - i) * sqheight, sqwidth, sqheight);
                        if (View == "Black")
                            rec = new Rectangle((8 - j) * sqwidth, (i + 1) * sqheight, sqwidth, sqheight);
                        eGfx.DrawImage(Piece, rec, 0, 0, 128, 128, GraphicsUnit.Pixel, attr);
                    }
                }
        }

        private void DrawTakenPieces()
        {
            Image Piece = null;
            int counter = 1;
            WhitePiecesTaken.Sort(); BlackPiecesTaken.Sort();
            foreach (Object ChessPiece in WhitePiecesTaken)
            {
                    Piece = null;
                    Piece = GetImage((int) ChessPiece);
                    if (Piece != null)
                    {
                        Rectangle rec = new Rectangle((int) (20 - counter) * sqwidth * 2/4, (int) ((9.6) * sqheight)  , sqwidth, sqheight);                        
                        eGfx.DrawImage(Piece, rec, 0, 0, 128, 128, GraphicsUnit.Pixel, attr);
                    }
                    counter++;
            }
            counter = 1;
            foreach (Object ChessPiece in BlackPiecesTaken)
            {
                Piece = null;
                Piece = GetImage((int)ChessPiece);
                if (Piece != null)
                {
                    Rectangle rec = new Rectangle((int) (20 - counter) * sqwidth * 2 / 4, (int)((10.4) * sqheight), sqwidth, sqheight);
                    eGfx.DrawImage(Piece, rec, 0, 0, 128, 128, GraphicsUnit.Pixel, attr);
                }
                counter++;
            }
        }

        private void DrawScore()
        {            
            Pen bluePen = new Pen(Color.Blue);            
            Pen redPen = new Pen(Color.Red);
            int counter = 1; int thisScore = 0; int prevScore = 0;     
            
            eGfx.DrawLine(redPen,  (int) (sqwidth * 2 / 4)      , (int)((11.5) * sqheight), 
                                   (int) (sqwidth * 2 / 4) + 300,     (int)((11.5) * sqheight));
            eGfx.DrawLine(redPen, (int)(sqwidth * 2 / 4), (int)((10.8) * sqheight),
                                  (int)(sqwidth * 2 / 4), (int)((12.2) * sqheight)); 
            foreach (Object movescore in Scoregraph)            
            {
                thisScore = (int) movescore;
                eGfx.DrawLine(bluePen, (int)(sqwidth * 2 / 4) + counter,     (int)((11.5) * sqheight) + (prevScore / 20), 
                                       (int)(sqwidth * 2 / 4) + counter + 1, (int)((11.5) * sqheight) + (thisScore / 20));              
                prevScore = thisScore;
                counter++;
            }           
        }

        private string StateOfPlaytoString()
        {
            string stateofplay = "BlackKingHasMoved: " + gamestate.BlackKingHasMoved.ToString() +
                            "\r\nBlackKRookHasMoved: " + gamestate.BlackKRookHasMovedOrTaken.ToString() +
                            "\r\nblackPawns2stepColumn: " + gamestate.blackPawns2stepColumn.ToString() +
                            "\r\nBlackQRookHasMoved: " + gamestate.BlackQRookHasMovedOrTaken.ToString() +
                            "\r\nWhiteKingHasMoved: " + gamestate.WhiteKingHasMoved.ToString() +
                            "\r\nWhiteKRookHasMoved: " + gamestate.WhiteKRookHasMovedOrTaken.ToString() +
                            "\r\nwhitePawns2stepColumn: " + gamestate.whitePawns2stepColumn.ToString() +
                            "\r\nWhiteQRookHasMoved: " + gamestate.WhiteQRookHasMovedOrTaken.ToString();
            return (stateofplay);
        }

        private Image GetImage(int chesspiece)
        {
            Image Piece = null;
            switch (chesspiece)
            {
                case White_Pawn: Piece = WhitePawn; break;
                case White_Bishop: Piece = WhiteBishop; break;
                case White_Rook: Piece = WhiteRook; break;
                case White_Knight: Piece = WhiteKnight; break;
                case White_King: Piece = WhiteKing; break;
                case White_Queen: Piece = WhiteQueen; break;
                case Black_Pawn: Piece = BlackPawn; break;
                case Black_Bishop: Piece = BlackBishop; break;
                case Black_Rook: Piece = BlackRook; break;
                case Black_Knight: Piece = BlackKnight; break;
                case Black_King: Piece = BlackKing; break;
                case Black_Queen: Piece = BlackQueen; break;
                case Empty: Piece = null; break;
            }
            return (Piece);
        }

        private string ConvertNumtoString(int number)
        {
            string ns = "";
            switch (number)
            {
                case 1: ns = "a"; break;
                case 2: ns = "b"; break;
                case 3: ns = "c"; break;
                case 4: ns = "d"; break;
                case 5: ns = "e"; break;
                case 6: ns = "f"; break;
                case 7: ns = "g"; break;
                case 8: ns = "h"; break;
            }
            return (ns);
        }

        private void frmMChild_Chess_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int[,] ProposedPositions = ChessPositions;
                if (e.Button == MouseButtons.Left)
                {
                    Point tempclicked = new Point((int)(e.X / sqwidth), 9 - (int)(e.Y / sqheight));
                    offset.X = ((int)e.X) - (tempclicked.X * sqwidth);
                    offset.Y = ((int)e.Y) - ((9 - tempclicked.Y) * sqheight);
                    //MessageBox.Show(WhitePlayer.Text.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (View == "Black")
                        tempclicked = new Point(9 - (int)(e.X / sqwidth), (int)(e.Y / sqheight));
                    if ((tempclicked.X > 0) && (tempclicked.X < 9) && (tempclicked.Y > 0) && (tempclicked.Y < 9))
                        if ((WhitePiece(tempclicked, ProposedPositions) && (true/*WhitePlayer.Text.ToString() == "Human"*/)) || 
                            (BlackPiece(tempclicked, ProposedPositions) && (true/*BlackPlayer.Text.ToString() == "Human"*/)))
                        {
                            piecegrabbed = true;
                            clicked.X = (int)(e.X / sqwidth);
                            clicked.Y = 9 - (int)(e.Y / sqheight);
                            if (View == "Black")
                            {
                                clicked.X = 9 - (int)(e.X / sqwidth);
                                clicked.Y = (int)(e.Y / sqheight);
                            }
                            draggedpiece = ChessPositions[clicked.Y - 1, clicked.X - 1];
                        }
                }
                //Automatic play
                //if (e.Button == MouseButtons.Right)
                //{
                //    if(!autoplay) autoplay = true;
                //    else autoplay = false;
                //    square.X = 8; square.Y = 8; clicked.X = 8; clicked.Y = 8;
                //    //autoplaytimer.Enabled = true;
                //}
                //if ((e.X < sqwidth) && (e.Y < sqheight))
                //    if (View == "White") View = "Black";
                //    else View = "White";
                Invalidate();
            }
            catch { MessageBox.Show("Mousedown", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void frmMChild_Chess_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if(!autoplay)
                {
                    Point sqold = square;
                    mouseposition.X = (int)e.X; mouseposition.Y = (int)e.Y;
                    square.X = (int)(e.X / sqwidth);
                    square.Y = 9 - (int)(e.Y / sqheight);
                    if (View == "Black")
                    {
                        square.X = 9 - (int)(e.X / sqwidth);
                        square.Y = (int)(e.Y / sqheight);
                    }
                    Invalidate();
                }
            }
            catch { MessageBox.Show("Mouse Move", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private bool WhitePiece(Point square, int[,] ProposedPositions)
        {
            if ((ProposedPositions[square.Y - 1, square.X - 1] >= White_King) && (ProposedPositions[square.Y - 1, square.X - 1] <= White_Pawn))
                return true;
            else return false;
        }

        private bool BlackPiece(Point square, int[,] ProposedPositions)
        {            
            if ((ProposedPositions[square.Y - 1, square.X - 1] >= Black_King) && (ProposedPositions[square.Y - 1, square.X - 1] <= Black_Pawn))
                return true;
            else return false;
        }

        private void frmMChild_Chess_MouseUp(object sender, MouseEventArgs e)
        {
            piecegrabbed = false;
            int colour = White;
            if ((square != clicked) && (clicked.X != 0) && (square.X > 0) && (square.Y > 0) && (square.X < 9) && (square.Y < 9)
                && (clicked.X > 0) && (clicked.Y > 0) && (clicked.X < 9) && (clicked.Y < 9))
            {

                if ((WhitePiece(clicked, ChessPositions) && (WhitePlayer.Text.ToString() == "Human")/* && (turn == White)*/) ||
                    (BlackPiece(clicked, ChessPositions) && (BlackPlayer.Text.ToString() == "Human")/* && (turn == Black)*/))
                    if (ProcessMove(false))
                    {
                        if (BlackPiece(square, ChessPositions)) colour = Black;
                        if (WhitePiece(square, ChessPositions)) colour = White;
                        turn = colour;
                        if (colour == Black) {blacklastmove_start = clicked; blacklastmove_finish = square;}
                        else {whitelastmove_start = clicked; whitelastmove_finish = square;}
                        if (((colour == White) && (BlackPlayer.Text.ToString() != "Human")) ||
                            ((colour == Black) && (WhitePlayer.Text.ToString() != "Human")))
                        {
                            ComputerPlay(-1);
                            if (BlackPiece(square, ChessPositions)) colour = Black;
                            if (WhitePiece(square, ChessPositions)) colour = White;
                            turn = colour;
                            if (colour == Black) { blacklastmove_start = clicked; blacklastmove_finish = square; }
                            else { whitelastmove_start = clicked; whitelastmove_finish = square; }
                        }
                        ShowECOmoves();                    
                    }
            }
            Invalidate();
        }

        private void ShowECOmoves()
        {
            ListECOmoves();
            ECOMovesTextBox.Text = ECOmovesavailable;
            foreach (ECOCode ecocode in ECOCodeList)
                if (ecocode.moves.Length <= moverecordECO.Length)
                    if (ecocode.moves == moverecordECO.Substring(0, ecocode.moves.Length))
                        ECOmovelist.Text = ecocode.Name;

        }

        private int oppcolour(int Colour)
        {
            if (Colour == White) return Black; else return White;
        }

        private void ComputerPlay(int forcecolour)
        {
            moving = true;
            int depthAI1 = (int) AI1_Depth.Value;
            int depthAI2 = (int) AI2_Depth.Value;            
            int AI = AI1;
            int depth = 4;
            int colour = White;
            Point KillerStart = new Point(0, 0);
            Point KillerFinish = new Point(0, 0);
            bool even = false;
            int piecescount = 10000;            
            //if (ProcessMove(false))
            {                
                lastmove_start = clicked;
                lastmove_finish = square;
                Invalidate();
                try
                {
                    piecescount = PiecesLeft(White, ChessPositions, gamestate);
                    StopWatch = DateTime.Now;
                    bestmoves = "";
                    AI1_Quiesce = AI1_Q.Checked; AI2_Quiesce = AI2_Q.Checked;
                    AI1_QDepth = (int) QDepth_AI1.Value; AI2_QDepth = (int) QDepth_AI2.Value;
                    AI1_PST = PST_AI1.Checked; AI2_PST = PST_AI2.Checked;
                    AI1_NS = Negascout_AI1.Checked; AI1_ItD = IterDeepening_AI1.Checked;
                    AI2_NS = Negascout_AI2.Checked; AI2_ItD = IterDeepening_AI2.Checked;
                    AI1_KH = KH_AI1.Checked; AI2_KH = KH_AI2.Checked;
                    addrandom = Randomise.Checked;
        
                    TreeNode Analysis = new TreeNode("Analysis");
                    AnalysisTreeView_AI1.Sort();
                    AnalysisTreeView_AI2.Sort();
                    thinking = true;
                    if (BlackPiece(square, ChessPositions)) colour = Black;
                    if (WhitePiece(square, ChessPositions)) colour = White;
                    if (forcecolour != -1) colour = forcecolour;
                    if (colour == Black) {blacklastmove_start = clicked; blacklastmove_finish = square;}
                    else {whitelastmove_start = clicked; whitelastmove_finish = square;}
                    if (Math.Floor((double) games/2) == (double) games/2) even = true;
                        else even = false;
                    /*if (((even) && (colour == Black)) || ((!even) && (colour == White)))
                        depth = 4;
                    else depth = 4;*/
                    if (movenumber > 7)
                        middlegame = true;
                    if(piecescount < 30)
                        if (((even) && (colour == Black)) || ((!even) && (colour == White)))
                        {                            
                            endgame = true;
                        }
                    if (piecescount < 15)
                        if (((even) && (colour == Black)) || ((!even) && (colour == White)))
                        {
                            //depth = 6;
                            endendgame = true;
                        }
                    if (piecescount < 6)
                        if (((even) && (colour == Black)) || ((!even) && (colour == White)))
                        {
                            //depth = 7;
                            endgame = true;                            
                        }
                    
                    if(InCheck(oppcolour(colour), ChessPositions, ref gamestate, AI1)) //Search extension, if in check increase depth by 1
                    { 
                        if(Ch_AI1.Checked) depthAI1++; 
                        if(Ch_AI2.Checked) depthAI2++;
                    }
                    if (endgame) { if (SE_AI1.Checked) depthAI1++; if (SE_AI2.Checked) depthAI2++; }//Search extension, if in endgame phase increase depth by 1
                    if (even)
                    {
                        if (colour == White) { depth = depthAI1; AI = AI1; }
                        else {depth = depthAI2; AI = AI2;}
                    }
                    if (!even)
                    {
                        if (colour == Black) { depth = depthAI1; AI = AI1; }
                        else {depth = depthAI2; AI = AI2;}
                    }
                    currentAI = AI;
                    winningamount = CalculateValue(colour, ChessPositions, new Point(0, 0), new Point(0, 0), AI, gamestate);
                    if (colour == Black) score = CalculateNextMove(depth, depth, White, ChessPositions, ref gamestate, -100000, 100000, White, ref Analysis, 0, AI, ref KillerStart, ref KillerFinish, -winningamount, new Point(0, 0), new Point(0, 0));    //Computer is White                                            
                    if (colour == White) score = CalculateNextMove(depth, depth, Black, ChessPositions, ref gamestate, -100000, 100000, Black, ref Analysis, 0, AI, ref KillerStart, ref KillerFinish, -winningamount, new Point(0, 0), new Point(0, 0));    //Computer is Black                    
                    Invalidate();
                    thinking = false;
                    if (colour == White) currentscore.Text = (-score).ToString();
                    if (colour == Black) currentscore.Text = score.ToString();
                    if (AI == AI1) AI1_totaltime += movetime;
                    if (AI == AI2) AI2_totaltime += movetime;
                    AI1_ptime.Text = (AI1_totaltime).ToString();
                    AI2_ptime.Text = (AI2_totaltime).ToString();
                    TimePerMove.Text = ((AI1_totaltime + AI2_totaltime) / cummulativemoves).ToString() +"msecs";
                    movetime = 0;
                    if (((score == 100000) && (colour == White)) || ((score == -100000) && (colour == Black)))   //BlackPiece(square, ChessPositions)))
                    {
                        if (autoplay) { checkmate = true; winner = "Black"; }
                        else MessageBox.Show("Checkmate, Black has certain win", "Logic Navigator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                        /*if (stalematecounter > 24)
                            MessageBox.Show("Stalemate", "Logic Navigator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/
                    if (((score == -100000) && (colour == White)) || ((score == 100000) && (colour == Black)))
                    {
                        if (autoplay) { checkmate = true; winner = "White"; }
                        else MessageBox.Show("Checkmate, White has certain win", "Logic Navigator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }                    
                    //if(clicked.X == 0)
                        //MessageBox.Show("null move calculation", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (clicked.X != 0)
                        if (ValidMoveOptimised(ChessPositions, gamestate, clicked, square, AI))
                            if (ProcessMove(true))
                            {
                                secondlastmove_start = clicked; secondlastmove_finish = square;
                            }
                            else MessageBox.Show("Problem with 'ProcessMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                    
                    movenumber++; cummulativemoves++;
                    if (AI == AI1)
                    {
                        AnalysisTreeView_AI1.Nodes.Clear();
                        AnalysisTreeView_AI1.Nodes.Add(Analysis);
                    }
                    if (AI == AI2)
                    {
                        AnalysisTreeView_AI2.Nodes.Clear();
                        AnalysisTreeView_AI2.Nodes.Add(Analysis);
                    }
                    timestamp = DateTime.Now.ToString();
                    analysis = bestmoves;
                    if(movenumber == 80)
                        winningamount = CalculateValue(colour, ChessPositions, new Point(0, 0), new Point(0, 0), AI, gamestate); 
                    //Invalidate();
                }
                catch { MessageBox.Show("Could not calculate best move, Start/Finish" + clicked.ToString() + square.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                
            }
            moving = false;
        }

        private bool ProcessMove(bool computercalc)
        {
            int[,] NewPositions = new int[8, 8] { 
            { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};
            
            StateofPlay SOP;
            
            string transition = ""; bool showstart = true;
            if (ValidMoveOptimised(ChessPositions, gamestate, clicked, square, AI1))
            {
                if ((ChessPositions[square.Y - 1, square.X - 1] == Empty) && (ChessPositions[clicked.Y - 1, clicked.X - 1] != White_Pawn) && (ChessPositions[clicked.Y - 1, clicked.X - 1] != Black_Pawn))
                {
                    stalematecounter++;
                    textBox1.Text = stalematecounter.ToString();
                }
                else stalematecounter = 0;
                if (WhitePiece(clicked, ChessPositions)) Scoregraph.Add(score);
                if (BlackPiece(clicked, ChessPositions)) Scoregraph.Add(score);
                if (WhitePiece(square, ChessPositions)) WhitePiecesTaken.Add(ChessPositions[square.Y - 1, square.X - 1]);
                if (BlackPiece(square, ChessPositions)) BlackPiecesTaken.Add(ChessPositions[square.Y - 1, square.X - 1]);

                if (ChessPositions[square.Y - 1, square.X - 1] != Empty) transition = "x"; else transition = "-";
                if ((ChessPositions[clicked.Y - 1, clicked.X - 1] == White_Pawn) || (ChessPositions[clicked.Y - 1, clicked.X - 1] == Black_Pawn))
                    showstart = false;
                else showstart = true;
                MovePiece(ChessPositions, ref gamestate, clicked, square, true, computercalc);
                DuplicateBoard(ChessPositions, NewPositions);
                SOP = DuplicateSOP(gamestate);
                GameBoard.Add(NewPositions); GameSOP.Add(SOP);        
                if (showstart) moverecord = moverecord +  ConvertNumtoString(clicked.X).ToString() + clicked.Y.ToString() + transition;
                moverecord = moverecord + ConvertNumtoString(square.X).ToString() + square.Y.ToString();
                moverecordECO += ConvertNumtoString(clicked.X).ToString() + clicked.Y.ToString() + ConvertNumtoString(square.X).ToString() + square.Y.ToString() + " ";
                if (BlackPiece(square, ChessPositions)) moverecord = moverecord + "\r\n";
                else moverecord = moverecord + ", ";
                if (turn == Black) TurnBox.Text = "Black";
                if (turn == White) TurnBox.Text = "White";
                return (true);
            }
            else
            {
                //MessageBox.Show("Problem with 'ProcessMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return (false);
            }
        }

        /************** AI Section ***************************************/

        private int CalculateNextMove(int depth, int top, int Colour, int[,] Board, ref StateofPlay GameState, int Alpha, int Beta, int MaxPlayer, ref TreeNode AnalysisTree, int takepiece,
            int AI, ref Point killerSt, ref Point killerFin, int boardvalue, Point Start, Point Finish)
        {
            int[,] ProposedPositions = new int[8, 8] { 
            { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};

            bool[,] PossibleEndPlaces = new bool[8, 8];
            //{ 
            //{true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, 
            //{true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}};

            //DuplicateBoard(Board, ProposedPositions);
            //Array.Copy(Board, ProposedPositions, 64);

            //Point Start = new Point(0, 0), Finish = new Point(0, 0);
            int value = 0;
            int delta = 0;
            //int[,] best3moves = new int[4, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            if (depth == 0)
            {
                if (AI == AI1) value = Quiesce(Colour, Board, GameState, Start, Finish, MaxPlayer, Alpha, Beta, takepiece, AI1_QDepth, AI, boardvalue);
                if (AI == AI2) value = Quiesce(Colour, Board, GameState, Start, Finish, MaxPlayer, Alpha, Beta, takepiece, AI2_QDepth, AI, boardvalue);
                TreeNode leafnode = new TreeNode(value.ToString());
                AnalysisTree.Nodes.Add(leafnode);
                return (value);
            }

            Point BestStart = new Point(0, 0);
            Point BestFinish = new Point(0, 0);
            Point KillerStart = new Point(0, 0);
            Point KillerFinish = new Point(0, 0);
            bool cutoff = false; int cutoffcounter = 1;
            StateofPlay SOP = DuplicateSOP(GameState);
            string moveinfo = "";
            bool donebyTree = false;
            bool InECObook = false;
            int takepiecesize = 0; int piece;
            ArrayList cutofflist = new ArrayList();
            int oppcolour = 0;
            if (Colour == White) oppcolour = Black; else oppcolour = White;

            bool searchPV = true;
            int newAlpha = Alpha;
            if ((top == depth) && ((AI1_ItD && (AI == AI1)) || (AI2_ItD && (AI == AI2))) && !ECOmoveAvailable())
            {
                try
                {
                    if (Colour == White) { lastmove_finish = blacklastmove_finish; lastmove_start = blacklastmove_start; secondlastmove_finish = whitelastmove_finish; secondlastmove_start = whitelastmove_start; }
                    else { lastmove_finish = whitelastmove_finish; lastmove_start = whitelastmove_start; secondlastmove_finish = blacklastmove_finish; secondlastmove_start = blacklastmove_start; }
                    TreeView AnalysisTreeView;
                    if (AI == AI1)
                        AnalysisTreeView = AnalysisTreeView_AI1;
                    else
                        AnalysisTreeView = AnalysisTreeView_AI2;
                    foreach (TreeNode topnode in AnalysisTreeView.Nodes)
                        foreach (TreeNode tn1 in topnode.Nodes)
                        {
                            string slstring = tn1.Text;
                            int startmoveindex = slstring.IndexOf("(");
                            if ((slstring[1 + startmoveindex].ToString() == secondlastmove_start.X.ToString()) && (slstring[3 + startmoveindex].ToString() == secondlastmove_start.Y.ToString()) &&
                                   (slstring[7 + startmoveindex].ToString() == secondlastmove_finish.X.ToString()) && (slstring[9 + startmoveindex].ToString() == secondlastmove_finish.Y.ToString()))
                                foreach (TreeNode tn2 in tn1.Nodes)
                                {
                                    string lstring = tn2.Text;
                                    int finishmoveindex = lstring.IndexOf("(");
                                    if ((lstring[1 + finishmoveindex].ToString() == lastmove_start.X.ToString()) && (lstring[3 + finishmoveindex].ToString() == lastmove_start.Y.ToString()) &&
                                        (lstring[7 + finishmoveindex].ToString() == lastmove_finish.X.ToString()) && (lstring[9 + finishmoveindex].ToString() == lastmove_finish.Y.ToString()))
                                        for (int i = tn2.Nodes.Count - 1; i >= 0; i--)
                                        {
                                            string move = tn2.Nodes[i].Text;
                                            if ((move.IndexOf("c") == 0))// || (move.IndexOf("a") == 0)) //cutoff or take piece, they should already be in the correct order.
                                            {
                                                cutofflist.Add(move);
                                                int moveindex = move.IndexOf("(");
                                                Start.X = (int)move[1 + moveindex] - 48; Start.Y = (int)move[3 + moveindex] - 48; //48 = '0' in character language
                                                Finish.X = (int)move[7 + moveindex] - 48; Finish.Y = (int)move[9 + moveindex] - 48;
                                                DuplicateBoard(Board, ProposedPositions);
                                                //DuplicateBoard(Board, ParentBoard);
                                                SOP = DuplicateSOP(GameState);
                                                if (ValidMoveOptimised(ProposedPositions, SOP, Start, Finish, AI) && (Start != Finish))
                                                {
                                                    cutoff = false; takepiecesize = ProposedPositions[Finish.Y - 1, Finish.X - 1];
                                                    MovePiece(ProposedPositions, ref SOP, Start, Finish, true, true);
                                                    delta = GetDeltaValue(Colour, Board, ProposedPositions, Start, Finish, AI);
                                                    if (depth == top) { Array.Copy(ProposedPositions, ChessPositionsbeingconsidered, 64); Startconsidered = Start; Finishconsidered = Finish; }
                                                    //for (int t = depth; t > 4; t--) bestmoves = bestmoves + "\t";
                                                    //if (depth > 5)
                                                    //    bestmoves = bestmoves + "Considering move:" + depth.ToString() + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")";
                                                    TreeNode childnode = new TreeNode("(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")");
                                                    {
                                                        if ((searchPV) && (((AI1_NS && (AI == AI1)) || (AI2_NS && (AI == AI2)))))
                                                            value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                        else
                                                        {
                                                            value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -newAlpha - 1, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                            if (value > newAlpha)
                                                                value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                        }
                                                        if (value >= Beta)
                                                        {
                                                            killerFin = Finish; killerSt = Start; return (Beta);
                                                        }
                                                        
                                                        if (value > newAlpha) { newAlpha = value; BestStart = Start; BestFinish = Finish; cutoff = true; }
                                                        //if(AI == AI1)
                                                            PopulateAnalysistree(moveinfo, depth, top, newAlpha, Beta, value, takepiecesize, Start, Finish, cutoff, ref cutoffcounter, ref childnode, ref AnalysisTree, StopWatch);
                                                    }
                                                }
                                            }
                                        }
                                }
                        }
                }
                catch { MessageBox.Show("Failure of level 1 treenode search", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }

            if (((AI1_KH && (AI == AI1)) || (AI2_KH && (AI == AI2))) && !ECOmoveAvailable()) //Killer Heuristic
            {
                try
                {
                    Start = killerSt; Finish = killerFin;
                    DuplicateBoard(Board, ProposedPositions);
                    SOP = DuplicateSOP(GameState);
                    if ((Start.X != 0) && (Finish.X != 0) && (Start.Y != 0) && (Finish.Y != 0))
                        if (ValidMoveOptimised(ProposedPositions, SOP, Start, Finish, AI) && (Start != Finish))
                        {
                            string move = "(" + killerSt.X.ToString() + "," + killerSt.Y.ToString() + ")" + "," + "(" + killerFin.X.ToString() + "," + killerFin.Y.ToString() + ")";
                            cutofflist.Add(move);
                            cutoff = false; takepiecesize = ProposedPositions[Finish.Y - 1, Finish.X - 1];
                            MovePiece(ProposedPositions, ref SOP, Start, Finish, true, true);
                            delta = GetDeltaValue(Colour, Board, ProposedPositions, Start, Finish, AI);
                            //if (depth == top) { Array.Copy(ProposedPositions, ChessPositionsbeingconsidered, 64); Startconsidered = Start; Finishconsidered = Finish; }
                            //for (int t = depth; t > 4; t--) bestmoves = bestmoves + "\t";
                            //if (depth > 5)
                            //    bestmoves = bestmoves + "Considering move:" + depth.ToString() + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")";
                            TreeNode childnode = new TreeNode("(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")");
                            {
                                if ((searchPV) && (((AI1_NS && (AI == AI1)) || (AI2_NS && (AI == AI2)))))
                                    value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                else
                                {
                                    value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -newAlpha - 1, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                    if (value > newAlpha)
                                        value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                }
                                if (value >= Beta)
                                {
                                    killerFin = Finish; killerSt = Start; return (Beta);
                                }
                                if (value > newAlpha) { newAlpha = value; BestStart = Start; BestFinish = Finish; cutoff = true; }
                                //if (AI == AI1)
                                    PopulateAnalysistree(moveinfo, depth, top, newAlpha, Beta, value, takepiecesize, Start, Finish, cutoff, ref cutoffcounter, ref childnode, ref AnalysisTree, StopWatch);
                            }/**/
                        }
                }
                catch { MessageBox.Show("Failure of level 1 treenode search", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }


            if (((top == depth + 1) && ((AI1_ItD && (AI == AI1)) || (AI2_ItD && (AI == AI2)))) && !ECOmoveAvailable())
            {
                try
                {
                    if (Colour == White) { lastmove_finish = blacklastmove_finish; lastmove_start = blacklastmove_start; secondlastmove_finish = whitelastmove_finish; secondlastmove_start = whitelastmove_start; }
                    else { lastmove_finish = whitelastmove_finish; lastmove_start = whitelastmove_start; secondlastmove_finish = blacklastmove_finish; secondlastmove_start = blacklastmove_start; }
                    TreeView AnalysisTreeView;
                    if (AI == AI1)
                        AnalysisTreeView = AnalysisTreeView_AI1;
                    else
                        AnalysisTreeView = AnalysisTreeView_AI2;
                    foreach (TreeNode topnode in AnalysisTreeView.Nodes)
                        foreach (TreeNode tn1 in topnode.Nodes)
                        {
                            string slstring = tn1.Text;
                            int startmoveindex = slstring.IndexOf("(");
                            if ((slstring[1 + startmoveindex].ToString() == secondlastmove_start.X.ToString()) && (slstring[3 + startmoveindex].ToString() == secondlastmove_start.Y.ToString()) &&
                                   (slstring[7 + startmoveindex].ToString() == secondlastmove_finish.X.ToString()) && (slstring[9 + startmoveindex].ToString() == secondlastmove_finish.Y.ToString()))
                                foreach (TreeNode tn2 in tn1.Nodes)
                                {
                                    string lstring = tn2.Text;
                                    int finishmoveindex = lstring.IndexOf("(");
                                    if ((lstring[1 + finishmoveindex].ToString() == lastmove_start.X.ToString()) && (lstring[3 + finishmoveindex].ToString() == lastmove_start.Y.ToString()) &&
                                        (lstring[7 + finishmoveindex].ToString() == lastmove_finish.X.ToString()) && (lstring[9 + finishmoveindex].ToString() == lastmove_finish.Y.ToString()))
                                        foreach (TreeNode tn3 in tn2.Nodes)
                                        {
                                            string nextstring = tn3.Text;
                                            string analstring = AnalysisTree.Text;
                                            int nextmoveindex = nextstring.IndexOf("(");
                                            int analmoveindex = analstring.IndexOf("(");
                                            if ((nextstring[1 + nextmoveindex].ToString() == analstring[1 + analmoveindex].ToString()) && (nextstring[3 + nextmoveindex].ToString() == analstring[3 + analmoveindex].ToString()) &&
                                                (nextstring[7 + nextmoveindex].ToString() == analstring[7 + analmoveindex].ToString()) && (nextstring[9 + nextmoveindex].ToString() == analstring[9 + analmoveindex].ToString()))
                                                for (int i = tn3.Nodes.Count - 1; i >= 0; i--)
                                                {
                                                    string move = tn3.Nodes[i].Text;
                                                    if ((move.IndexOf("c") == 0))// || (move.IndexOf("a") == 0)) //cutoff or take piece, they should already be in the correct order.
                                                    {
                                                        cutofflist.Add(move);
                                                        int moveindex = move.IndexOf("(");
                                                        Start.X = (int)move[1 + moveindex] - 48; Start.Y = (int)move[3 + moveindex] - 48; //48 = '0' in character language
                                                        Finish.X = (int)move[7 + moveindex] - 48; Finish.Y = (int)move[9 + moveindex] - 48;
                                                        DuplicateBoard(Board, ProposedPositions);
                                                        SOP = DuplicateSOP(GameState);
                                                        if (ValidMoveOptimised(ProposedPositions, SOP, Start, Finish, AI) && (Start != Finish))
                                                        {
                                                            cutoff = false; takepiecesize = ProposedPositions[Finish.Y - 1, Finish.X - 1];
                                                            MovePiece(ProposedPositions, ref SOP, Start, Finish, true, true);
                                                            delta = GetDeltaValue(Colour, Board, ProposedPositions, Start, Finish, AI);
                                                            if (depth == top) { Array.Copy(ProposedPositions, ChessPositionsbeingconsidered, 64); Startconsidered = Start; Finishconsidered = Finish; }
                                                            for (int t = depth; t > 4; t--) bestmoves = bestmoves + "\t";
                                                            if (depth > 5)
                                                                bestmoves = bestmoves + "Considering move:" + depth.ToString() + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")";
                                                            TreeNode childnode = new TreeNode("(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")");
                                                            {
                                                                if ((searchPV) && (((AI1_NS && (AI == AI1)) || (AI2_NS && (AI == AI2)))))
                                                                    value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                                else
                                                                {
                                                                    value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -newAlpha - 1, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                                    if (value > newAlpha)
                                                                        value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                                }
                                                                if (value >= Beta)
                                                                {
                                                                    killerFin = Finish; killerSt = Start; return (Beta);
                                                                }
                                                                if (value > newAlpha) { newAlpha = value; BestStart = Start; BestFinish = Finish; cutoff = true; }
                                                                //if (AI == AI1)
                                                                    PopulateAnalysistree(moveinfo, depth, top, newAlpha, Beta, value, takepiecesize, Start, Finish, cutoff, ref cutoffcounter, ref childnode, ref AnalysisTree, StopWatch);
                                                            }
                                                        }
                                                    }
                                                }
                                        }
                                }
                        }
                }
                catch { MessageBox.Show("Failure of level 2 treenode search, " + AnalysisTree.Text, "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }


            //if ((AI1_MoveorderingD1 && (AI == AI1)) || (AI2_MoveorderingD1 && (AI == AI2)))
            //{
            //    if (top == depth)
            //    {
            //        try
            //        {
            //            Start = clicked;
            //            Finish = square;
            //            DuplicateBoard(Board, ProposedPositions);
            //            SOP = DuplicateSOP(GameState);
            //            if (ValidMove(ProposedPositions, SOP, Start, Finish) && (Start != Finish))
            //            {
            //                cutoff = false; takepiecesize = ProposedPositions[Finish.Y - 1, Finish.X - 1];
            //                MovePiece(ProposedPositions, ref SOP, Start, Finish, true, true);
            //                if (depth == top) { Array.Copy(ProposedPositions, ChessPositionsbeingconsidered, 64); Startconsidered = Start; Finishconsidered = Finish; }
            //                for (int t = depth; t > 4; t--) bestmoves = bestmoves + "\t";
            //                if (depth > 5)
            //                    bestmoves = bestmoves + "Considering move:" + depth.ToString() + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")";
            //                TreeNode childnode = new TreeNode("(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")");
            //                {
            //                    value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -Alpha, MaxPlayer, ref childnode, takepiecesize, AI);

            //                    if (value >= Beta) return (Beta);
            //                    if (value > Alpha) { Alpha = value; BestStart = Start; BestFinish = Finish; cutoff = true; }
            //                    //PopulateAnalysistree(moveinfo, depth, top, Alpha, Beta, value, takepiecesize, Start, Finish, cutoff, ref cutoffcounter, ref childnode, ref AnalysisTree, StopWatch);
            //                }
            //            }
            //        }
            //        catch { MessageBox.Show("Failure of level 1 treenode search", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            //    }
            //}
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Start.X = i + 1; Start.Y = j + 1;
                    DuplicateBoard(Board, ProposedPositions);
                    SOP = DuplicateSOP(GameState);
                    if (WhitePiece(Start, ProposedPositions) && (Colour == White) ||
                        BlackPiece(Start, ProposedPositions) && (Colour == Black))
                    {
                        piece = ProposedPositions[Start.Y - 1, Start.X - 1];
                        FindPossibleMoves(Start, ProposedPositions[Start.Y - 1, Start.X - 1], ProposedPositions, ref PossibleEndPlaces);

                        for (int l = 0; l < 8; l++)
                            for (int m = 0; m < 8; m++)
                            {

                                if (PossibleEndPlaces[m, l])
                                //    impossiblemove = false;
                                //else
                                //    impossiblemove = true;
                                //if (!impossiblemove)
                                {
                                    Finish.X = l + 1; Finish.Y = m + 1;
                                    donebyTree = false;
                                    if (((AI1_ItD && (AI == AI1)) || (AI2_ItD && (AI == AI2))))
                                        foreach (Object cutoffmove in cutofflist)
                                        {
                                            string move = (string)cutoffmove;
                                            int moveindex = move.IndexOf("(");
                                            if ((Start.X == (int)move[1 + moveindex] - 48) &&
                                               (Start.Y == (int)move[3 + moveindex] - 48) &&
                                               (Finish.X == (int)move[7 + moveindex] - 48) &&
                                               (Finish.Y == (int)move[9 + moveindex] - 48))
                                                donebyTree = true;
                                        }
                                    if (top == depth)
                                    {
                                        if (ECOmoveAvailable())
                                        {
                                            if (MoveisinECOBook(Start, Finish))
                                                InECObook = true; //If there is an ECO move available and this is not one of them, then dont consider this move.
                                            else InECObook = false;
                                        }
                                        else InECObook = true;
                                    }
                                    if ((!donebyTree) && (InECObook || (top != depth)))
                                    {
                                        //if ((Start.X == 5) && (Start.Y == 6) && (Finish.X == 4) && (Finish.Y == 5) && (depth == 3))
                                            //InECObook = InECObook;
                                        DuplicateBoard(Board, ProposedPositions);
                                        SOP = DuplicateSOP(GameState);
                                        if (ValidMoveOptimised(ProposedPositions, SOP, Start, Finish, AI) && (Start != Finish))
                                        {
                                     //       if (!PossibleEndPlaces[m, l])
                                       //         cutoff = cutoff;
                                            loopcount2++;
                                            cutoff = false; takepiecesize = ProposedPositions[Finish.Y - 1, Finish.X - 1];
                                            MovePiece(ProposedPositions, ref SOP, Start, Finish, true, true);
                                            delta = GetDeltaValue(Colour, Board, ProposedPositions, Start, Finish, AI);
                                            if (depth == top)
                                            { Array.Copy(ProposedPositions, ChessPositionsbeingconsidered, 64); Startconsidered = Start; Finishconsidered = Finish; }
                                            //for (int t = depth; t > 4; t--) bestmoves = bestmoves + "\t";
                                            //if (depth > 5)
                                            //    bestmoves = bestmoves + "Considering move:" + depth.ToString() + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")";
                                            TreeNode childnode = new TreeNode("(" + Start.X.ToString() + "," + Start.Y.ToString() + ")" + "," + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")");
                                            {
                                                if ((searchPV) && (((AI1_NS && (AI == AI1)) || (AI2_NS && (AI == AI2)))))
                                                    value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                else
                                                {
                                                    value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -newAlpha - 1, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                    if (value > newAlpha)
                                                        value = -CalculateNextMove(depth - 1, top, oppcolour, ProposedPositions, ref SOP, -Beta, -newAlpha, MaxPlayer, ref childnode, takepiecesize, AI, ref KillerStart, ref KillerFinish, -boardvalue - delta, Start, Finish);
                                                }
                                                if (value >= Beta)
                                                {
                                                    killerFin = Finish; killerSt = Start; return (Beta);
                                                }
                                                if (value > newAlpha) { newAlpha = value; BestStart = Start; BestFinish = Finish; cutoff = true; }
                                                //if (AI == AI1)
                                                    PopulateAnalysistree(moveinfo, depth, top, newAlpha, Beta, value, takepiecesize, Start, Finish, cutoff, ref cutoffcounter, ref childnode, ref AnalysisTree, StopWatch);
                                            }
                                        }
                                        else
                                            loopcount++;
                                    }
                                }
                            }
                    }
                }
            if (depth == top)
            {
                clicked = BestStart;
                square = BestFinish;
                if (BestFinish.X == 0)
                    BestFinish = BestStart;
            }

            if (depth == top - 1)
            {
                killerstart = BestStart;
                killerend = BestFinish;
            }
            
            return (newAlpha);
            
            /*if (depth == 1)
            {
                difference = DateTime.Now - StopWatch;
                timestamps[0] = (int)difference.TotalMilliseconds;
            }*/

        }


        private int Quiesce(int Colour, int[,] Board, StateofPlay GameState, Point start, Point finish, int MaxPlayer, int Alpha, int Beta, int takepiece, int qdepth, int AI, int boardvalue)
        {
            //int value = CalculateValue(Colour, Board, start, finish, AI);
            //if (value != boardvalue) 
              //  value = value;
            int stand_pat = 0;
            if (IncrementalBoardEvaluation.Checked)
            {
                int CalculateMobilityv;// = CalculateMobility(Board);
                //if (AI == AI1)
                   CalculateMobilityv = 0;
                if ((qdepth == 0))
                    return boardvalue + CalculateMobilityv;// (Board); //feedmobility;//boardvalue + feedmobility;//CalculateValue(Colour, Board, start, finish, AI);

                if (((AI == AI1) && (AI1_Quiesce == false)) || ((AI == AI2) && (AI2_Quiesce == false)))
                    return boardvalue + CalculateMobilityv;//;//CalculateValue(Colour, Board, start, finish, AI);

                if (takepiece == Empty)
                    return boardvalue + CalculateMobilityv;//;//CalculateValue(Colour, Board, start, finish, AI);

                stand_pat = boardvalue + CalculateMobilityv;////CalculateValue(Colour, Board, start, finish, AI);
            }
            else
            {

                if ((qdepth == 0))
                    return CalculateValue(Colour, Board, start, finish, AI, GameState);

                if (((AI == AI1) && (AI1_Quiesce == false)) || ((AI == AI2) && (AI2_Quiesce == false)))
                    return CalculateValue(Colour, Board, start, finish, AI, GameState);

                if (takepiece == Empty)
                    return CalculateValue(Colour, Board, start, finish, AI, GameState);

                stand_pat = CalculateValue(Colour, Board, start, finish, AI, GameState);
            }
            int new_alpha = Alpha;
            if (stand_pat >= Beta)
                return (Beta);
            if (new_alpha < stand_pat)
                new_alpha = stand_pat;

            int[,] ProposedPositions = new int[8, 8];
            int delta = 0;
            //{ 
            //{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};
                
            bool[,] PossibleEndPlaces = new bool[8, 8];
            //{ 
            //{true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, 
            //{true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}};

            //DuplicateBoard(Board, ProposedPositions);
            StateofPlay SOP;// = DuplicateSOP(GameState);
            Point Start = new Point(0, 0), Finish = new Point(0, 0); int piece, score;
            int oppcolour = 0;
            if (Colour == White) oppcolour = Black; else oppcolour = White;

            int mobility = 0;
            //mobility = CalculateMobility(Board);
            if(AI == AI1)
            {

            }
            
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Start.X = i + 1; Start.Y = j + 1;
                    DuplicateBoard(Board, ProposedPositions);
                    SOP = DuplicateSOP(GameState);
                    if (WhitePiece(Start, ProposedPositions) && (Colour == White) ||
                        BlackPiece(Start, ProposedPositions) && (Colour == Black))
                    {
                        piece = ProposedPositions[Start.Y - 1, Start.X - 1];
                        FindPossibleMoves(Start, ProposedPositions[Start.Y - 1, Start.X - 1], ProposedPositions, ref PossibleEndPlaces);

                        for (int l = 0; l < 8; l++)
                            for (int m = 0; m < 8; m++)
                            {
                                Finish.X = l + 1; Finish.Y = m + 1;
                                {
                                    piece = ProposedPositions[Start.Y - 1, Start.X - 1];
                                    if (PossibleEndPlaces[m, l])
                                    {
                                        DuplicateBoard(Board, ProposedPositions);                                        
                                        SOP = DuplicateSOP(GameState);
                                        if ((WhitePiece(Finish, ProposedPositions) && (Colour == Black) || //take move
                                            (BlackPiece(Finish, ProposedPositions) && (Colour == White))))
                                            if (((AI == AI1) && ValidMoveOptimised(ProposedPositions, SOP, Start, Finish, AI)) || ((AI == AI2) && ValidMoveOptimised(ProposedPositions, SOP, Start, Finish, AI)))
                                            {
                                                loopcount2++;
                                                MovePiece(ProposedPositions, ref SOP, Start, Finish, true, true);
                                                delta = GetDeltaValue(Colour, Board, ProposedPositions, Start, Finish, AI);
                                                score = -(mobility + Quiesce(oppcolour, ProposedPositions, SOP, Start, Finish, MaxPlayer, -Beta, -new_alpha, 1, qdepth - 1, AI, -boardvalue - delta));
                                                if (score >= Beta)
                                                {
                                                    return (Beta);
                                                }
                                                if (score > new_alpha) new_alpha = score;
                                            }
                                            else
                                                loopcount++;
                                    }
                                }
                            }
                    }
                }
            return new_alpha;

        }

        private int CalculateMobility(int[,] Board)
        {
            int mobility = 0;
            Point Start = new Point();
            int piece = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Start.X = i + 1; Start.Y = j + 1;
                    piece = Board[j, i];
                    if ((piece == Black_Bishop) || (piece == White_Bishop))
                        mobility += CalculateBishopMobility(Start, Board, piece);
                    if (middlegame)
                    {
                        //if ((piece == Black_Rook) || (piece == White_Rook))
                        //    mobility += CalculateRookMobility(Start, Board, piece);
                        //if ((piece == Black_Queen) || (piece == White_Queen))
                        //    mobility += CalculateQueenMobility(Start, Board, piece);
                    }
                }     
            if(piece > Black_Pawn)
                return(mobility);
            else return (-mobility);
        }

        private void PopulateAnalysistree(string moveinfo, int depth, int top, int Alpha, int Beta, int value, int takepiecesize, Point Start, Point Finish, bool cutoff, ref int cutoffcounter, ref TreeNode childnode, ref TreeNode AnalysisTree, DateTime StopWatch)
        {

            TimeSpan difference;
            //if (depth > 4)
            //{
            //    moveinfo = ", (Alpha:" + Alpha.ToString() + ", Beta:" + Beta.ToString() + ")\r\n";
            //bestmoves = bestmoves + moveinfo;
            //}
            if (depth > top - 4)
            {
                moveinfo = "";
                if (cutoff) moveinfo = "cutoff[" + cutoffcounter++ + "], "; else moveinfo = "";
                if (!cutoff && (takepiecesize != Empty)) moveinfo = "a" + takepiecesize.ToString();
                moveinfo = moveinfo + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")";
                if (takepiecesize != Empty) moveinfo = moveinfo + "x"; else moveinfo = moveinfo + ",";
                moveinfo = moveinfo + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")" + ", (Alpha:" + Alpha.ToString() + ", Beta:" + Beta.ToString() + ", Value:" + value.ToString() + ")";
                TreeNode branchnode = new TreeNode(moveinfo);
                foreach (TreeNode tn in childnode.Nodes)
                    branchnode.Nodes.Add(tn);
                AnalysisTree.Nodes.Add(branchnode);
            }
            if (depth > top - 1)
            {
                difference = DateTime.Now - StopWatch;
                bestmoves = bestmoves + difference.TotalMilliseconds.ToString() + " msec\r\n";
                movetime = (int)difference.TotalMilliseconds;
            }

            //TimeSpan difference;
            //if (depth > 5)
            //{
            //    moveinfo = ", (Alpha:" + Alpha.ToString() + ", Beta:" + Beta.ToString() + ")\r\n";
            //    bestmoves = bestmoves + moveinfo;
            //}

            //if (FullAnalysis.Checked)
            //{
            //    if (depth > top - 7)
            //    {
            //        moveinfo = "";
            //        if (cutoff) moveinfo = "cutoff[" + cutoffcounter++ + "], "; else moveinfo = "";
            //        if (!cutoff && (takepiecesize != Empty)) moveinfo = "a" + takepiecesize.ToString();
            //        moveinfo = moveinfo + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")";
            //        if (takepiecesize != Empty) moveinfo = moveinfo + "x"; else moveinfo = moveinfo + ",";
            //        moveinfo = moveinfo + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")" + ", (Alpha:" + Alpha.ToString() + ", Beta:" + Beta.ToString() + ", Value:" + value.ToString() + ")";
            //        TreeNode branchnode = new TreeNode(moveinfo);
            //        foreach (TreeNode tn in childnode.Nodes)
            //            branchnode.Nodes.Add(tn);
            //        AnalysisTree.Nodes.Add(branchnode);
            //    }
            //}
            //if (depth > top - 1)
            //{
            //    difference = DateTime.Now - StopWatch;
            //    bestmoves = bestmoves + difference.TotalMilliseconds.ToString() + " msec\r\n";
            //    movetime = (int) difference.TotalMilliseconds;
            //}


            //TimeSpan difference;
            //if (depth > 5)
            //{
            //    moveinfo = ", (Alpha:" + Alpha.ToString() + ", Beta:" + Beta.ToString() + ")\r\n";
            //    bestmoves = bestmoves + moveinfo;
            //}
            //if (depth > top - 5)
            //{
            //    moveinfo = "";
            //    if (cutoff) moveinfo = "cutoff[" + cutoffcounter++ + "], "; else moveinfo = "";
            //    if (!cutoff && (takepiecesize != Empty)) moveinfo = "a" + takepiecesize.ToString();
            //    moveinfo = moveinfo + "(" + Start.X.ToString() + "," + Start.Y.ToString() + ")";
            //    if (takepiecesize != Empty) moveinfo = moveinfo + "x"; else moveinfo = moveinfo + ",";
            //    moveinfo = moveinfo + "(" + Finish.X.ToString() + "," + Finish.Y.ToString() + ")" + ", (Alpha:" + Alpha.ToString() + ", Beta:" + Beta.ToString() + ", Value:" + value.ToString() + ")";
            //    TreeNode branchnode = new TreeNode(moveinfo);
            //    foreach (TreeNode tn in childnode.Nodes)
            //        branchnode.Nodes.Add(tn);
            //    AnalysisTree.Nodes.Add(branchnode);
            //}
            //if (depth > top - 1)
            //{
            //    difference = DateTime.Now - StopWatch;
            //    bestmoves = bestmoves + difference.TotalMilliseconds.ToString() + " msec\r\n";
            //    movetime = (int)difference.TotalMilliseconds;
            //}

        }

        private void FindPossibleMoves(Point Start, int piece, int[,] ProposedPositions, ref bool[,] PossibleEndPlaces)
        {
            //bool[,] Ones = new bool[8, 8] { 
            //{true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, 
            //{true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}};
            
            //Array.Copy(Ones, PossibleEndPlaces, 64);
            //if (!checkBox1.Checked)
            {
                switch (piece)
                {
                    case White_Pawn: CalculatePawn(Start, piece, ref PossibleEndPlaces); break;
                    case White_Bishop: CalculateBishop(Start, ProposedPositions, ref PossibleEndPlaces); break;
                    case White_Rook: CalculateRook(Start, ProposedPositions, ref PossibleEndPlaces); break;
                    case White_Knight: CalculateKnight(Start, ref PossibleEndPlaces); break;
                    case White_King: CalculateKing(Start, piece, ref PossibleEndPlaces); break;
                    case White_Queen: CalculateQueen(Start, ProposedPositions, ref  PossibleEndPlaces); break;
                    case Black_Pawn: CalculatePawn(Start, piece, ref PossibleEndPlaces); break;
                    case Black_Bishop: CalculateBishop(Start, ProposedPositions, ref PossibleEndPlaces); ; break;
                    case Black_Rook: CalculateRook(Start, ProposedPositions, ref PossibleEndPlaces); break;
                    case Black_Knight: CalculateKnight(Start, ref PossibleEndPlaces); break;
                    case Black_King: CalculateKing(Start, piece, ref PossibleEndPlaces); break;
                    case Black_Queen: CalculateQueen(Start, ProposedPositions, ref  PossibleEndPlaces); break;
                }
            }
            //else
            //{
            //    switch (piece)
            //    {
            //        case White_Pawn: CalculatePawn(Start, White, ref PossibleEndPlaces); break;
            //        case White_Bishop: CalculateBishop(Start, ProposedPositions, ref PossibleEndPlaces); break;
            //        case White_Rook: CalculateRook(Start, Colour, ProposedPositions, ref PossibleEndPlaces); break;
            //        case White_Knight: CalculateKnight(Start, ref PossibleEndPlaces); break;
            //        case White_King: CalculateKing(Start, Colour, ref PossibleEndPlaces); break;
            //        case White_Queen: CalculateQueen(Start, ProposedPositions, ref  PossibleEndPlaces); break;
            //        case Black_Pawn: CalculatePawn(Start, Black, ref PossibleEndPlaces); break;
            //        case Black_Bishop: CalculateBishop(Start, ProposedPositions, ref PossibleEndPlaces); ; break;
            //        case Black_Rook: CalculateRook(Start, Colour, ProposedPositions, ref PossibleEndPlaces); break;
            //        case Black_Knight: CalculateKnight(Start, ref PossibleEndPlaces); break;
            //        case Black_King: CalculateKing(Start, Colour, ref PossibleEndPlaces); break;
            //        case Black_Queen: CalculateQueen(Start, ProposedPositions, ref  PossibleEndPlaces); break;
            //    }
            //}
        }
                                
        private void CalculatePawn(Point Start, int piece, ref bool[,] PossibleEndPlaces)
        {
            //Find where the pawn can go
            //bool[,] Zeros = new bool[8, 8] {  
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
            Array.Copy(Zerosglobal, PossibleEndPlaces, 64);
            if (piece == White_Pawn)
            {
                if (Start.Y < 8) PossibleEndPlaces[(Start.Y + 1) - 1, Start.X - 1] = true;
                if (Start.Y == 2) PossibleEndPlaces[(Start.Y + 2) - 1, Start.X - 1] = true;
                if (Start.X > 1) PossibleEndPlaces[(Start.Y + 1) - 1, (Start.X - 1) - 1] = true;
                if (Start.X < 8) PossibleEndPlaces[(Start.Y + 1) - 1, (Start.X + 1) - 1] = true;
            }
            else
            {
                if (Start.Y > 1) PossibleEndPlaces[(Start.Y - 1) - 1, Start.X - 1] = true;
                if (Start.Y == 7) PossibleEndPlaces[(Start.Y - 2) - 1, Start.X - 1] = true;
                if (Start.X > 1) PossibleEndPlaces[(Start.Y - 1) - 1, (Start.X - 1) - 1] = true;
                if (Start.X < 8) PossibleEndPlaces[(Start.Y - 1) - 1, (Start.X + 1) - 1] = true;
            }
        }

        private void CalculateKnight(Point Start, ref bool[,] PossibleEndPlaces)
        {
            //Find where the Knight can go
            int[,] MoveMatrix = new int[8, 2] { { 2, 1 }, { 1, 2 }, { -2, 1 }, { -1, 2 }, { 2, -1 }, { 1, -2 }, { -2, -1 }, { -1, -2 } };

            //bool[,] Zeros = new bool[8, 8] {  
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
            Array.Copy(Zerosglobal, PossibleEndPlaces, 64);
            //bool blockpiece = false;
            //if ((Start.X == 3) && (Start.Y == 6))
            //    blockpiece = false;
            for (int i = 0; i < 8; i++)
            {
                MoveMatrix[i, 0] = MoveMatrix[i, 0] + Start.X;
                MoveMatrix[i, 1] = MoveMatrix[i, 1] + Start.Y;
            }
            for (int i = 0; i < 8; i++)
            {
                if ((MoveMatrix[i, 0] > 8) || (MoveMatrix[i, 0] < 1)) 
                {
                    MoveMatrix[i, 0] = Start.X; MoveMatrix[i, 1] = Start.Y;
                }
                if ((MoveMatrix[i, 1] > 8) || (MoveMatrix[i, 1] < 1))
                {
                    MoveMatrix[i, 0] = Start.X; MoveMatrix[i, 1] = Start.Y;
                }
            }
            for (int i = 0; i < 8; i++)
                PossibleEndPlaces[MoveMatrix[i, 1] - 1, MoveMatrix[i, 0] - 1] = true;            
        }

        private void CalculateKing(Point Start, int piece, ref bool[,] PossibleEndPlaces)
        {
            //Find where the King can go
            int[,] MoveMatrix = new int[8, 2] { { -1, 1 }, { 0, 1 }, { 1, 1 }, { -1, -1 }, { 0, -1 }, { 1, -1 }, { -1, 0 }, { 1, 0 } };

            //bool[,] Zeros = new bool[8, 8] {  
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
            //Array.Copy(Zerosglobal, PossibleEndPlaces, 64);

            for (int i = 0; i < 8; i++)
            {
                MoveMatrix[i, 0] = MoveMatrix[i, 0] + Start.X;
                MoveMatrix[i, 1] = MoveMatrix[i, 1] + Start.Y;
            }
            for (int i = 0; i < 8; i++)
            {
                if ((MoveMatrix[i, 0] > 8) || (MoveMatrix[i, 0] < 1))
                {
                    MoveMatrix[i, 0] = Start.X; MoveMatrix[i, 1] = Start.Y;
                }
                if ((MoveMatrix[i, 1] > 8) || (MoveMatrix[i, 1] < 1))
                {
                    MoveMatrix[i, 0] = Start.X; MoveMatrix[i, 1] = Start.Y;
                }
            }
            for (int i = 0; i < 8; i++)
                PossibleEndPlaces[MoveMatrix[i, 1] - 1, MoveMatrix[i, 0] - 1] = true;
            if ((piece == White_King) && (Start.Y == 1) && (Start.X == 5))
            { PossibleEndPlaces[1 - 1, 7 - 1] = true; PossibleEndPlaces[1 - 1, 3 - 1] = true;}
            if ((piece == Black_King) && (Start.Y == 8) && (Start.X == 5))
            { PossibleEndPlaces[8 - 1, 7 - 1] = true; PossibleEndPlaces[8 - 1, 3 - 1] = true;}            
        }

        private int CalculateQueenMobility(Point Start, int[,] Board, int piece)
        {
            int mobility = 0;
            int endpiece = 0;
            bool blocked = false;
            for (int i = Start.X + 1; i < 9; i++)
                if (!blocked)
                    if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                    {
                        endpiece = Board[i + (Start.Y - Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                        else mobility++;
                    }
            blocked = false;
            for (int i = Start.X - 1; i > 0; i--)
                if (!blocked)
                    if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                    {
                        endpiece = Board[i + (Start.Y - Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                    }
            blocked = false;
            for (int i = Start.X + 1; i < 9; i++)
                if (!blocked)
                    if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                    {
                        endpiece = Board[-i + (Start.Y + Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                    }
            blocked = false;
            for (int i = Start.X - 1; i > 0; i--)
                if (!blocked)
                    if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                    {
                        endpiece = Board[-i + (Start.Y + Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                    }
            for (int i = Start.X + 1; i < 9; i++)
                if (!blocked)
                {
                    endpiece = Board[Start.Y - 1, i - 1];
                    if (endpiece != Empty)
                    {
                        mobility += MobilityElement(piece, endpiece);
                        blocked = true;
                    }
                    else mobility++;
                }
            blocked = false;
            for (int i = Start.X - 1; i > 0; i--)
                if (!blocked)
                {
                    endpiece = Board[Start.Y - 1, i - 1];
                    if (endpiece != Empty)
                    {
                        mobility += MobilityElement(piece, endpiece);
                        blocked = true;
                    }
                    else mobility++;
                }
            blocked = false;
            for (int j = Start.Y + 1; j < 9; j++)
                if (!blocked)
                {
                    endpiece = Board[j - 1, Start.X - 1];
                    if (endpiece != Empty)
                    {
                        mobility += MobilityElement(piece, endpiece);
                        blocked = true;
                    }
                    else mobility++;
                }
            blocked = false;
            for (int j = Start.Y - 1; j > 0; j--)
                if (!blocked)
                {
                    endpiece = Board[j - 1, Start.X - 1];
                    if (endpiece != Empty)
                    {
                        mobility += MobilityElement(piece, endpiece);
                        blocked = true;
                    }
                    else mobility++;
                }            
            return mobility;
        }


        private int CalculateBishopMobility(Point Start, int [,] Board, int piece)
        {
            int mobility = 0;
            int endpiece = 0;
            bool blocked = false;
            for (int i = Start.X + 1; i < 9; i++)
                if(!blocked)
                    if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                    {
                        endpiece = Board[i + (Start.Y - Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                        else mobility++;
                    }
            blocked = false;
            for (int i = Start.X - 1; i > 0; i--)
                if (!blocked)
                    if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                    {
                        endpiece = Board[i + (Start.Y - Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                    }
            blocked = false;
            for (int i = Start.X + 1; i < 9; i++)
                if (!blocked)
                    if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                    {
                        endpiece = Board[-i + (Start.Y + Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                    }
            blocked = false;
            for (int i = Start.X - 1; i > 0; i--)
                if (!blocked)
                    if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                    {
                        endpiece = Board[-i + (Start.Y + Start.X) - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                    }            
            return mobility;
        }


        private int CalculateRookMobility(Point Start, int[,] Board, int piece)
        {
            int mobility = 0;
            int endpiece = 0;
            bool blocked = false;
            for (int i = Start.X + 1; i < 9; i++)
                if(!blocked)
                {
                        endpiece = Board[Start.Y - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                        else mobility++;
                }
            blocked = false;
            for (int i = Start.X - 1; i > 0; i--)
                if(!blocked)
                {
                        endpiece = Board[Start.Y - 1, i - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                        else mobility++;
                }
            blocked = false;
            for (int j = Start.Y + 1; j < 9; j++)
                if(!blocked)
                {
                        endpiece = Board[j - 1, Start.X - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                        else mobility++;
                }            
            blocked = false;
            for (int j = Start.Y - 1; j > 0; j--)
                if(!blocked)
                {
                        endpiece = Board[j - 1, Start.X - 1];
                        if (endpiece != Empty)
                        {
                            mobility += MobilityElement(piece, endpiece);
                            blocked = true;
                        }
                        else mobility++;
                }
            return (mobility);

        }

        private int MobilityElement(int piece, int spot)
        {
            int mobilityelement = 0;
            if ((piece == White_Bishop) || (piece == White_Rook) || (piece == White_Queen))
            {
                if (spot > Black_Pawn)
                    mobilityelement--;
                //else mobilityelement += 3;
            }
            else
            {
                if (spot <= Black_Pawn)
                    mobilityelement--;
                //else mobilityelement += 3;
            }            
            return mobilityelement;
        }
        
        private void CalculateBishop(Point Start,  int[,] ProposedPositions, ref bool[,] PossibleEndPlaces)
        {  //////////////////////untested
            //Find where the Bishop can go
            bool blockpiece = false;
            //bool[,] Zeros = new bool[8, 8] {  
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
            Array.Copy(Zerosglobal, PossibleEndPlaces, 64); 
            {
                for (int i = Start.X + 1; i < 9; i++)
                    if(!blockpiece)
                        if((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                            if (ProposedPositions[i + (Start.Y - Start.X) - 1, i - 1] == Empty)
                                PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                            else
                            {
                                PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                blockpiece = false;
                for (int i = Start.X - 1; i > 0; i--)
                    if (!blockpiece)
                        if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                            if (ProposedPositions[i + (Start.Y - Start.X) - 1, i - 1] == Empty)
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;                            
                            else
                            {
                                PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                blockpiece = false;
                for (int i = Start.X + 1; i < 9; i++)
                    if (!blockpiece)
                        if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                            if (ProposedPositions[-i + (Start.Y + Start.X) - 1, i - 1] == Empty)
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;                        
                            else
                            {
                                PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                blockpiece = false;
                for (int i = Start.X - 1; i > 0; i--)
                    if (!blockpiece)        
                        if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                            if (ProposedPositions[-i + (Start.Y + Start.X) - 1, i - 1] == Empty)
                                PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;                            
                            else
                            {
                                PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
            }
        }

        private void CalculateRookOld(Point Start, int Colour, int[,] ProposedPositions, ref bool[,] PossibleEndPlaces)
        {
            for (int k = 0; k < 3000; k++)
            {
                //Find where the rook can go
                bool blockpiece = false;
            //    bool[,] Zeros = new bool[8, 8] {  
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
                Array.Copy(Zerosglobal, PossibleEndPlaces, 64);
                for (int i = Start.X + 1; i < 9; i++)
                    if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                    }
                    else
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                        blockpiece = true; //run into another piece
                    }
                blockpiece = false;
                for (int i = Start.X - 1; i > 0; i--)
                    if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                    }
                    else
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                        blockpiece = true; //run into another piece
                    }
                blockpiece = false;
                for (int j = Start.Y + 1; j < 9; j++)
                    if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                    }
                    else
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                        blockpiece = true; //run into another piece
                    }
                blockpiece = false;
                for (int j = Start.Y - 1; j > 0; j--)
                    if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                    }
                    else
                    {
                        if (!blockpiece)
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                        blockpiece = true; //run into another piece
                    }
            }
        }
                
        private void CalculateRook(Point Start, int[,] ProposedPositions, ref bool[,] PossibleEndPlaces)
        {
            //for (int k = 0; k < 3000; k++) //Optimisation code
            {
                //Find where the rook can go
                bool blockpiece = false;
            //    bool[,] Zeros = new bool[8, 8] {  
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
                Array.Copy(Zerosglobal, PossibleEndPlaces, 64);
                for (int i = Start.X + 1; i < 9; i++)
                    if (!blockpiece)
                        if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                        else
                        {
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                blockpiece = false;
                for (int i = Start.X - 1; i > 0; i--)
                    if (!blockpiece)
                        if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                        else
                        {
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                blockpiece = false;
                for (int j = Start.Y + 1; j < 9; j++)
                    if (!blockpiece)
                        if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                        else
                        {
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                blockpiece = false;
                for (int j = Start.Y - 1; j > 0; j--)
                    if (!blockpiece)
                        if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                        else
                        {
                            PossibleEndPlaces[j - 1, Start.X - 1] = true;
                            blockpiece = true; //run into another piece
                        }
            }
        }

        private void CalculateQueenOld(Point Start, int[,] ProposedPositions, ref bool[,] PossibleEndPlaces)
        {
            //for (int k = 0; k < 3000; k++) //Optimisation code
            {
                //Find where the Queen can go
                bool blockpiece = false;
                bool[,] Zeros = new bool[8, 8] {  
            {false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            {false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
                Array.Copy(Zeros, PossibleEndPlaces, 64);//Diagonals (Bishop moves)
                {
                    for (int i = Start.X + 1; i < 9; i++)
                        if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                            if (ProposedPositions[i + (Start.Y - Start.X) - 1, i - 1] == Empty)
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                            }
                            else
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                    blockpiece = false;
                    for (int i = Start.X - 1; i > 0; i--)
                        if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                            if (ProposedPositions[i + (Start.Y - Start.X) - 1, i - 1] == Empty)
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                            }
                            else
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                    blockpiece = false;
                    for (int i = Start.X + 1; i < 9; i++)
                        if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                            if (ProposedPositions[-i + (Start.Y + Start.X) - 1, i - 1] == Empty)
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                            }
                            else
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                    blockpiece = false;
                    for (int i = Start.X - 1; i > 0; i--)
                        if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                            if (ProposedPositions[-i + (Start.Y + Start.X) - 1, i - 1] == Empty)
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                            }
                            else
                            {
                                if (!blockpiece)
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                    blockpiece = false;/// Files (Rook moves)
                    for (int i = Start.X + 1; i < 9; i++)
                        if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                        }
                        else
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                    blockpiece = false;
                    for (int i = Start.X - 1; i > 0; i--)
                        if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                        }
                        else
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                    blockpiece = false;
                    for (int j = Start.Y + 1; j < 9; j++)
                        if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                        }
                        else
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                    blockpiece = false;
                    for (int j = Start.Y - 1; j > 0; j--)
                        if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                        }
                        else
                        {
                            if (!blockpiece)
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                    blockpiece = false;
                }
            }
        }


        private void CalculateQueen(Point Start, int[,] ProposedPositions, ref bool[,] PossibleEndPlaces)
        {  
            //for (int k = 0; k < 3000; k++) //Optimisation code
            {
                //Find where the Queen can go
                bool blockpiece = false;
            //    bool[,] Zeros = new bool[8, 8] {  
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},
            //{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false},{false,false,false,false,false,false,false,false}};
                Array.Copy(Zerosglobal, PossibleEndPlaces, 64);//Diagonals (Bishop moves)
                {
                    for (int i = Start.X + 1; i < 9; i++)
                        if (!blockpiece)
                            if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                                if (ProposedPositions[i + (Start.Y - Start.X) - 1, i - 1] == Empty)
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                else
                                {
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                    blockpiece = true; //run into another piece
                                }
                    blockpiece = false;
                    for (int i = Start.X - 1; i > 0; i--)
                        if (!blockpiece)
                            if ((i + (Start.Y - Start.X) < 9) && (i + (Start.Y - Start.X) > 0))
                                if (ProposedPositions[i + (Start.Y - Start.X) - 1, i - 1] == Empty)
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                else
                                {
                                    PossibleEndPlaces[i + (Start.Y - Start.X) - 1, i - 1] = true;
                                    blockpiece = true; //run into another piece
                                }
                    blockpiece = false;
                    for (int i = Start.X + 1; i < 9; i++)
                        if (!blockpiece)
                            if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                                if (ProposedPositions[-i + (Start.Y + Start.X) - 1, i - 1] == Empty)
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                else
                                {
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                    blockpiece = true; //run into another piece
                                }
                    blockpiece = false;
                    for (int i = Start.X - 1; i > 0; i--)
                        if (!blockpiece)
                            if ((-i + (Start.Y + Start.X) < 9) && (-i + (Start.Y + Start.X) > 0))
                                if (ProposedPositions[-i + (Start.Y + Start.X) - 1, i - 1] == Empty)
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                else
                                {
                                    PossibleEndPlaces[-i + (Start.Y + Start.X) - 1, i - 1] = true;
                                    blockpiece = true; //run into another piece
                                }
                    blockpiece = false;/// Files (Rook moves)
                    for (int i = Start.X + 1; i < 9; i++)
                        if (!blockpiece)
                            if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                                PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                            else
                            {
                                PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                    blockpiece = false;
                    for (int i = Start.X - 1; i > 0; i--)
                        if (ProposedPositions[Start.Y - 1, i - 1] == Empty)
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                        else
                        {
                            PossibleEndPlaces[Start.Y - 1, i - 1] = true;
                            blockpiece = true; //run into another piece
                        }
                    blockpiece = false;
                    for (int j = Start.Y + 1; j < 9; j++)
                        if (!blockpiece)
                            if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                            else
                            {
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                    blockpiece = false;
                    for (int j = Start.Y - 1; j > 0; j--)
                        if (!blockpiece)
                            if (ProposedPositions[j - 1, Start.X - 1] == Empty)
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                            else
                            {
                                PossibleEndPlaces[j - 1, Start.X - 1] = true;
                                blockpiece = true; //run into another piece
                            }
                }
            }
        }


        private int GetDeltaValue(int Colour, int[,] Board, int[,] ProposedBoard, Point start, Point finish, int AI)
        {
            try
            {
                int delta = 0;
                int[] squaresX = { 0, 0, 0, 0 };
                int[] squaresY = { 0, 0, 0, 0 };
                squaresX[0] = start.X;
                squaresY[0] = start.Y;
                squaresX[1] = finish.X;
                squaresY[1] = finish.Y;

                int movingpiece = Board[start.Y - 1, start.X - 1];

                if (movingpiece == White_King)
                {
                    // King Side Castle
                    if ((start.X == 5) && (finish.X == 7) && (start.Y == 1) && (finish.Y == 1))
                    { squaresX[2] = 8; squaresY[2] = 1; squaresX[3] = 6; squaresY[3] = 1; }
                    // Queens Side Castle
                    if ((start.X == 5) && (finish.X == 3) && (start.Y == 1) && (finish.Y == 1))
                    { squaresX[2] = 1; squaresY[2] = 1; squaresX[3] = 4; squaresY[3] = 1; }
                }
                if (movingpiece == Black_King)
                {
                    // King Side Castle
                    if ((start.X == 5) && (finish.X == 7) && (start.Y == 8))
                    { squaresX[2] = 8; squaresY[2] = 8; squaresX[3] = 6; squaresY[3] = 8; }
                    // Queens Side Castle
                    if ((start.X == 5) && (finish.X == 3) && (start.Y == 8))
                    { squaresX[2] = 1; squaresY[2] = 8; squaresX[3] = 4; squaresY[3] = 8; }
                }
                if (movingpiece == White_Pawn) //Enpassant         
                    if (finish.Y == 6) 
                        if(start.X != finish.X)
                        { squaresX[2] = finish.X; squaresY[2] = 5; }
                if (movingpiece == Black_Pawn) //Enpassant  
                    if(finish.Y == 3)
                        if(start.X != finish.X)
                        { squaresX[2] = finish.X; squaresY[2] = 4; }

                for (int j = 0; j < 4; j++)
                {
                    if (squaresX[j] != 0)
                    {
                        int chesspiece = Board[squaresY[j] - 1, squaresX[j] - 1];
                        delta -= value(Colour, chesspiece);
                        delta -= pieceSquareTable(Colour, chesspiece, squaresY[j] - 1, squaresX[j] - 1, AI);
                        chesspiece = ProposedBoard[squaresY[j] - 1, squaresX[j] - 1];
                        delta += value(Colour, chesspiece);
                        delta += pieceSquareTable(Colour, chesspiece, squaresY[j] - 1, squaresX[j] - 1, AI);
                    }
                }
                if (AI == AI1)
                    if (addrandom) delta += randObj.Next(4) - 2;
                if (AI == AI2)
                    if (addrandom) delta += randObj.Next(4) - 2;
                //if (Colour == White)
                    return (delta);
                //else
                //    return (-delta);
            }
            catch { MessageBox.Show("Problem with Delta Value " + start.ToString() + finish.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return (0);
        }

        private int value(int Colour, int chesspiece)
        {
            if (Colour == White)
            {
                switch (chesspiece)
                {
                    case Empty: return (0);
                    case White_Pawn: return (100); 
                    case White_Bishop: return (330); 
                    case White_Rook: return (500); 
                    case White_Knight: return (320); 
                    case White_Queen: return (900); 
                    case Black_Pawn: return (-100); 
                    case Black_Bishop: return (-330); 
                    case Black_Rook: return (-500); 
                    case Black_Knight: return (-320); 
                    case Black_Queen: return (-900); 
                }
            }
            else
            {
                switch (chesspiece)
                {
                    case Empty: return (0);
                    case White_Pawn: return (-100);
                    case White_Bishop: return (-330);
                    case White_Rook: return (-500);
                    case White_Knight: return (-320);
                    case White_Queen: return (-900);
                    case Black_Pawn: return (100);
                    case Black_Bishop: return (330);
                    case Black_Rook: return (500);
                    case Black_Knight: return (320);
                    case Black_Queen: return (900);
                }
            }
            return (0);
        }

        private int pieceSquareTable(int Colour, int chesspiece, int i, int j, int AI)
        {
            int value = 0;
            if (AI == AI1)
            {   //////////////////////Computer Player 1 (AI-1) ////////////////////////////
                switch (chesspiece)
                {
                    case White_Pawn: value += Pawn_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Bishop: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Rook: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Knight: value += Knight_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                    case Black_Pawn: value -= Pawn_PieceSquareTable[i, j]; break;
                    case Black_Bishop: value -= Bishop_PieceSquareTable[i, j]; break;
                    case Black_Rook: value -= Rook_PieceSquareTable[i, j]; break;
                    case Black_Knight: value -= Knight_PieceSquareTable[i, j]; break;
                    case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                } // minimum - 100%

                if (endgame)
                    switch (chesspiece)
                    {
                        case White_King: value += King_PieceSquareTable_endgame[7 - i, 7 - j]; break;
                        case Black_King: value -= King_PieceSquareTable_endgame[i, j]; ; break;
                    }
                else
                    if (middlegame)
                        switch (chesspiece)
                        {
                            case White_King: value += King_PieceSquareTable_middlegame[7 - i, 7 - j]; break;
                            case Black_King: value -= King_PieceSquareTable_middlegame[i, j]; break;
                            case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                            case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                        }
                    else
                        switch (chesspiece)
                        {
                            case White_King: value += King_PieceSquareTable_middlegame[7 - i, 7 - j]; break;
                            case Black_King: value -= King_PieceSquareTable_middlegame[i, j]; break;
                            case White_Queen: value += Queen_PieceSquareTable_notmiddlegame[7 - i, 7 - j]; break;
                            case Black_Queen: value -= Queen_PieceSquareTable_notmiddlegame[i, j]; break;
                        }
            }
            if (AI == AI2)
            {   //////////////////////Computer Player 1 (AI-2) ////////////////////////////
                switch (chesspiece)
                {
                    case White_Pawn: value += Pawn_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Bishop: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Rook: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Knight: value += Knight_PieceSquareTable[7 - i, 7 - j]; break;
                    case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                    case Black_Pawn: value -= Pawn_PieceSquareTable[i, j]; break;
                    case Black_Bishop: value -= Bishop_PieceSquareTable[i, j]; break;
                    case Black_Rook: value -= Rook_PieceSquareTable[i, j]; break;
                    case Black_Knight: value -= Knight_PieceSquareTable[i, j]; break;
                    case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                } // minimum - 100%

                if (endgame)
                    switch (chesspiece)
                    {
                        case White_King: value += King_PieceSquareTable_endgame[7 - i, 7 - j]; break;
                        case Black_King: value -= King_PieceSquareTable_endgame[i, j]; ; break;
                    }
                else
                    if (middlegame)
                        switch (chesspiece)
                        {
                            case White_King: value += King_PieceSquareTable_middlegame[7 - i, 7 - j]; break;
                            case Black_King: value -= King_PieceSquareTable_middlegame[i, j]; break;
                            case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                            case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                        }
                    else
                        switch (chesspiece)
                        {
                        }
            }
            if (Colour == White)
                return (value);
            else
                return (-value);
        }


        private int CalculateValue(int Colour, int[,] Board, Point start, Point finish, int AI, StateofPlay SOP)
        {

            //Point WhiteKingLocation = FindKing(White, Board);
            //Point BlackKingLocation = FindKing(Black, Board);
            int value = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    int chesspiece = Board[i, j];
                    if (chesspiece != Empty)
                    {
                        if(AI == AI1)//if (((even) && (Colour == Black)) || ((!even) && (Colour == White)))
                        {   //////////////////////Computer Player 1 (AI-1) ////////////////////////////
                            switch (chesspiece)
                            {
                                case White_Pawn: value = value + 100; break;
                                case White_Bishop: value = value + 330; break;
                                case White_Rook: value = value + 500; break;
                                case White_Knight: value = value + 320; break;
                                case White_Queen: value = value + 900; break;
                                case Black_Pawn: value = value - 100; break;
                                case Black_Bishop: value = value - 330; break;
                                case Black_Rook: value = value - 500; break;
                                case Black_Knight: value = value - 320; break;
                                case Black_Queen: value = value - 900; break;
                            } // minimum - 100%

                            if (AI1_PST)
                            {
                                switch (chesspiece)
                                {
                                    case White_Pawn: value += Pawn_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Bishop: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Rook: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Knight: value += Knight_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                                    case Black_Pawn: value -= Pawn_PieceSquareTable[i, j]; break;
                                    case Black_Bishop: value -= Bishop_PieceSquareTable[i, j]; break;
                                    case Black_Rook: value -= Rook_PieceSquareTable[i, j]; break;
                                    case Black_Knight: value -= Knight_PieceSquareTable[i, j]; break;
                                    case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                                } // minimum - 100%
                                /*
                                distance = 0;
                                if ((chesspiece == White_Bishop) || (chesspiece == White_Rook) || (chesspiece == White_Queen) || (chesspiece == White_Knight))
                                    distance -= (BlackKingLocation.X - j) + (BlackKingLocation.Y - i);
                                if ((chesspiece == Black_Bishop) || (chesspiece == Black_Rook) || (chesspiece == Black_Queen) || (chesspiece == Black_Knight))
                                    distance += (WhiteKingLocation.X - j) + (WhiteKingLocation.Y - i);
                                value += distance;
*/
                                /*
                                if (!endgame) // King Safety - Keep a pawn shield if king has castled // Does not seem to help
                                {
                                    if (SOP.WhiteKingHasMoved)
                                        if (chesspiece == White_King) 
                                            if (i < 7)
                                            {
                                                if (Board[i + 1, j] == White_Pawn)
                                                    value += 3;
                                                if (j < 7)
                                                    if (Board[i + 1, j + 1] == White_Pawn)
                                                        value += 3;
                                                if (j > 0)
                                                    if (Board[i + 1, j - 1] == White_Pawn)
                                                        value += 3;
                                            }
                                    if (SOP.WhiteKingHasMoved)
                                        if (chesspiece == Black_King)
                                            if (i > 0)
                                            {
                                                if (Board[i - 1, j] == Black_Pawn)
                                                    value -= 3;
                                                if (j < 7)
                                                    if (Board[i - 1, j + 1] == Black_Pawn)
                                                        value -= 3;
                                                if (j > 0)
                                                    if (Board[i - 1, j - 1] == Black_Pawn)
                                                        value -= 3;
                                            }
                                }*/ 

                                /*
                                if (chesspiece == White_Pawn) //Linked Pawn
                                {
                                    if (j < 7)
                                        if (Board[i + 1, j + 1] == White_Pawn)
                                            value += 3;
                                    if (j > 0)
                                        if (Board[i + 1, j - 1] == White_Pawn)
                                            value += 3;
                                }
                                if (chesspiece == Black_Pawn) //Linked Pawn
                                {
                                    if (j < 7)
                                        if (Board[i - 1, j + 1] == Black_Pawn)
                                            value -= 3;
                                    if (j > 0)
                                        if (Board[i - 1, j - 1] == Black_Pawn)
                                            value -= 3;
                                }
                                if (chesspiece == White_Pawn) // adds 13.6%
                                {
                                    bool doubled = false; bool passed = true;
                                    for (int k = i + 1; k < 7; k++)
                                    {
                                        if (Board[k, j] == White_Pawn) 
                                            doubled = true;
                                        if (Board[k, j] == Black_Pawn) 
                                            passed = false;
                                    }
                                    if (doubled) 
                                        //value = value - 3;
                                    if (passed) 
                                        value = value + 3;
                                }
                                if (chesspiece == Black_Pawn) // adds 13.6%
                                {
                                    bool doubled = false; bool passed = true;
                                    for (int k = i - 1; k > 0; k--)
                                    {
                                        if (Board[k, j] == Black_Pawn) 
                                            doubled = true;
                                        if (Board[k, j] == White_Pawn) 
                                            passed = false;
                                    }
                                    if (doubled) 
                                        //value = value + 3;
                                    if (passed) 
                                        value = value - 3;
                                }*/
                                // Hold queen back in early moves - Doesn't seem to make much difference
                                /*
                                if (movenumber < 15)
                                {
                                    if (chesspiece == White_Queen)
                                        if ((i != 1 - 1) || (j != 4 - 1))
                                            value = value - 30;
                                    if (chesspiece == Black_Queen)
                                        if ((i != 8 - 1) || (j != 4 - 1))
                                            value = value + 30;
                                }   
                                */
                                if (endgame)
                                    switch (chesspiece)
                                    { 
                                        case White_King: value += King_PieceSquareTable_endgame[7 - i, 7 - j]; break;
                                        case Black_King: value -= King_PieceSquareTable_endgame[i, j]; ; break;
                                    }
                                else
                                    if (middlegame)
                                        switch (chesspiece)
                                        {
                                            case White_King: value += King_PieceSquareTable_middlegame[7 - i, 7 - j]; break;
                                            case Black_King: value -= King_PieceSquareTable_middlegame[i, j]; break;
                                            case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                                            case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                                        }                                   
                            }   
                        }
                        else  //////////////////////Computer Player 2 (AI-2) ////////////////////////////
                        {
                            switch(chesspiece)
                            {
                                case White_Pawn: value = value + 100; break;
                                case White_Bishop: value = value + 330; break;
                                case White_Rook: value = value + 500; break;
                                case White_Knight: value = value + 320; break;
                                //case White_King: value += EdgeDistance[i,j]*10; break;
                                case White_Queen: value = value + 900; break;
                                case Black_Pawn: value = value - 100; break;
                                case Black_Bishop: value = value - 330; break;
                                case Black_Rook: value = value - 500; break;
                                case Black_Knight: value = value - 320; break;
                                //case Black_King: value -= EdgeDistance[i,j]*10; break;
                                case Black_Queen: value = value - 900; break;                            
                            } // minimum - 100%

                            if (AI2_PST)
                            {
                                switch (chesspiece)
                                {
                                    case White_Pawn: value += Pawn_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Bishop: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Rook: value += Bishop_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Knight: value += Knight_PieceSquareTable[7 - i, 7 - j]; break;
                                    case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                                    case Black_Pawn: value -= Pawn_PieceSquareTable[i, j]; break;
                                    case Black_Bishop: value -= Bishop_PieceSquareTable[i, j]; break;
                                    case Black_Rook: value -= Rook_PieceSquareTable[i, j]; ; break;
                                    case Black_Knight: value -= Knight_PieceSquareTable[i, j]; break;
                                    case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                                } // minimum - 100%
                                /*
                                distance = 0;
                                if ((chesspiece == White_Bishop) || (chesspiece == White_Rook) || (chesspiece == White_Queen) || (chesspiece == White_Knight) || (chesspiece == White_Pawn))
                                    distance -= (BlackKingLocation.X - j) + (BlackKingLocation.Y - i);
                                if ((chesspiece == Black_Bishop) || (chesspiece == Black_Rook) || (chesspiece == Black_Queen) || (chesspiece == Black_Knight) || (chesspiece == Black_Pawn))
                                    distance += (WhiteKingLocation.X - j) + (WhiteKingLocation.Y - i);
                                value += distance;
*//*
                                if (chesspiece == White_Pawn) //Linked Pawn
                                {
                                    if (j < 7)
                                        if (Board[i + 1, j + 1] == White_Pawn)
                                            value += 3;
                                    if (j > 0)
                                        if (Board[i + 1, j - 1] == White_Pawn)
                                            value += 3;
                                }
                                if (chesspiece == Black_Pawn) //Linked Pawn
                                {
                                    if (j < 7)
                                        if (Board[i - 1, j + 1] == Black_Pawn)
                                            value -= 3;
                                    if (j > 0)
                                        if (Board[i - 1, j - 1] == Black_Pawn)
                                            value -= 3;
                                }
                                if (chesspiece == White_Pawn) // adds 13.6%
                                {
                                    bool doubled = false; bool passed = true;
                                    for (int k = i + 1; k < 7; k++)
                                    {
                                        if (Board[k, j] == White_Pawn)
                                            doubled = true;
                                        if (Board[k, j] == Black_Pawn)
                                            passed = false;
                                    }
                                    if (doubled)
                                        value = value - 3;
                                    if (passed)
                                        value = value + 3;
                                }
                                if (chesspiece == Black_Pawn) // adds 13.6%
                                {
                                    bool doubled = false; bool passed = true;
                                    for (int k = i - 1; k > 0; k--)
                                    {
                                        if (Board[k, j] == Black_Pawn)
                                            doubled = true;
                                        if (Board[k, j] == White_Pawn)
                                            passed = false;
                                    }
                                    if (doubled)
                                        value = value + 3;
                                    if (passed)
                                        value = value - 3;
                                }*/
                                // Hold queen back in early moves                                
                                /*if (movenumber < 9)
                                {
                                     if (chesspiece == Black_Queen)
                                         if ((i != 8 - 1) || (j != 4 - 1))
                                             value = value + 20;
                                     if (chesspiece == White_Queen)
                                         if ((i != 1 - 1) || (j != 4 - 1))
                                             value = value - 20;
                                } */     
 
                                if (endgame)
                                    switch (chesspiece)
                                    {
                                        case White_King: value += King_PieceSquareTable_endgame[7 - i, 7 - j]; break;
                                        case Black_King: value -= King_PieceSquareTable_endgame[i, j]; ; break;
                                    }
                                else
                                    if (middlegame)
                                        switch (chesspiece)
                                        {
                                            case White_King: value += King_PieceSquareTable_middlegame[7 - i, 7 - j]; break;
                                            case Black_King: value -= King_PieceSquareTable_middlegame[i, j]; break;
                                            case White_Queen: value += Queen_PieceSquareTable[7 - i, 7 - j]; break;
                                            case Black_Queen: value -= Queen_PieceSquareTable[i, j]; break;
                                        }   
                            }
                        }

                        //Advance pawns
                        /*
                            if (chesspiece == White_Pawn)
                                value += i * 1;
                            if (chesspiece == Black_Pawn)
                                value += -(8 - i) * 1;*/

                        /*
                        if(endgame)    
                            switch (chesspiece)
                            {
                                case White_King: value += EdgeDistance[i,j]*100; break;
                                case Black_King: value -= EdgeDistance[i,j]*100; break;
                            } 
*/

                        /*
                        if(endgame)
                        {
                            int oppcolour;
                            if(Colour == Black) oppcolour = White; else oppcolour = Black;
                            int counter = 0;
                            Point King = new Point(0, 0); Point Kingto = new Point(0,0);
                            int[,] ProposedPositions = new int[8, 8] { 
                                        { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};

                            bool[,] PossibleEndPlaces = new bool[8, 8] { 
                                    {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, 
                                    {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}, {true,true,true,true,true,true,true,true}};

                            DuplicateBoard(Board, ProposedPositions);
                            StateofPlay SOP = DuplicateSOP(GameState);
                            King = FindKing(oppcolour, ProposedPositions);
                            FindPossibleMoves(oppcolour, King, ProposedPositions[King.Y - 1, King.X - 1], ProposedPositions, ref PossibleEndPlaces);   
                            for (int l = 0; l < 8; l++)
                                for (int m = 0; m < 8; m++)                                                                
                                    if (PossibleEndPlaces[m, l])
                                    {
                                        Kingto.X = l + 1; Kingto.Y = m + 1;
                                        DuplicateBoard(Board, ProposedPositions);
                                        SOP = DuplicateSOP(GameState);
                                        if (ValidMove(ProposedPositions, SOP, King, Kingto) && (King != Kingto))
                                            counter++;
                                    }
                            if(Colour == White) value = value + ((9 - counter) * 10);
                            else value = value - ((9 - counter) * 10);
                        }
                        */
                        /*
                         * 
                         * 
                         * 
                         * 
                        // This section applies to AI1
                       /*if ((chesspiece == Black_King) && (start.X == 5) && ((finish.X == 7) || (finish.X == 3)))
                            value = value - 500;
                        if ((chesspiece == White_King) && (start.X == 5) && ((finish.X == 7) || (finish.X == 3)))
                            value = value + 500; */
                        // Rook on the 7th rank
                        //if ((chesspiece == White_Rook) || (i == 7 - 1)) value = value + 10;//adds 1.9%
                        //if ((chesspiece == Black_Rook) || (i == 2 - 1)) value = value - 10;//adds 1.9%                                                
                        //Passed and Doubled Pawns
                        /*
                        if (chesspiece == White_Pawn) // adds 13.6%
                        {
                            bool doubled = false; bool passed = true;
                            for (int k = i + 1; k < 7; k++)
                            {
                                if (Board[k, j] == White_Pawn) doubled = true;
                                if (Board[k, j] == Black_Pawn) passed = false;
                            }
                            if (doubled) value = value - 10;
                            if (passed) value = value + 10;
                        }                       
                        if (chesspiece == Black_Pawn) // adds 13.6%
                        {
                            bool doubled = false; bool passed = true;
                            for (int k = i - 1; k > 0; k--)
                            {
                                if (Board[k, j] == Black_Pawn) doubled = true;
                                if (Board[k, j] == White_Pawn) passed = false;
                            }
                            if (doubled) value = value + 10;
                            if (passed) value = value - 10;
                        }      
                        // Reward Rooks that have a clear file
                        if (chesspiece == Black_Rook) 
                        {
                            bool clearfile = true;
                            for (int k = 0; k < 8; k++)                                
                                if((Board[k, j] != Empty) && (Board[k, j] != Black_Rook)) clearfile = false;
                            if(clearfile) value -= 10;
                            clearfile = true;
                            for (int k = 0; k < 8; k++)
                                if ((Board[i, k] != Empty) && (Board[i, k] != Black_Rook)) clearfile = false;
                            if (clearfile) value -= 10;
                        }
                        if (chesspiece == White_Rook)
                        {
                            bool clearfile = true;
                            for (int k = 0; k < 8; k++)
                                if ((Board[k, j] != Empty) && (Board[k, j] != White_Rook)) clearfile = false;
                            if (clearfile) value += 10;
                            clearfile = true;
                            for (int k = 0; k < 8; k++)
                                if ((Board[i, k] != Empty) && (Board[i, k] != White_Rook)) clearfile = false;
                            if (clearfile) value += 10;
                        }      */
                        /*                      
                       if (chesspiece == White_Knight)                            
                           if ((i != 1 - 1) && ((j != 2 - 1) || (j != 7 - 1)))
                               value = value - 2;
                       if (chesspiece == Black_Knight)
                           if ((i != 8 - 1) && ((j != 2 - 1) || (j != 7 - 1)))
                               value = value + 2;    */
                        // Hold queen back in early moves
                        /*
                        if (movenumber < 9)
                         {
                             if (chesspiece == Black_Queen)
                                 if ((i != 8 - 1) || (j != 4 - 1))
                                     value = value + 30;
                             if (chesspiece == White_Queen)
                                 if ((i != 1 - 1) || (j != 4 - 1))
                                     value = value - 30;
                         }  */              
                        /*if (chesspiece == Black_Queen)      //Control - do something stupid                      
                            value = value + 1000;
                        if (chesspiece == White_Queen)
                            value = value - 1000;
                        if (chesspiece == Black_Rook)      
                            value = value + 1000;
                        if (chesspiece == White_Rook)
                            value = value - 1000;*/
                    }
                }
            if (AI == AI1)
                if (addrandom) value += randObj.Next(4) - 2;
            if (AI == AI2)
                if (addrandom) value += randObj.Next(4) - 2;
            if(Colour == White)
                return (value);
            else
                return (-value);
        }

        private int PiecesLeft(int Colour, int[,] Board, StateofPlay GameState)
        {
            int value = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    int chesspiece = Board[i, j];
                    if (chesspiece != Empty)
                    {
                        switch (chesspiece)
                        {
                            case White_Pawn: value = value + 1; break;
                            case White_Bishop: value = value + 3; break;
                            case White_Rook: value = value + 5; break;
                            case White_Knight: value = value + 3; break;
                            //case White_King: value = value; break;
                            case White_Queen: value = value + 9; break;
                            case Black_Pawn: value = value + 1; break;
                            case Black_Bishop: value = value + 3; break;
                            case Black_Rook: value = value + 5; break;
                            case Black_Knight: value = value + 3; break;
                            //case Black_King: value = value; break;
                            case Black_Queen: value = value + 9; break;
                        }
                    }
                }
            if (value > 100)
                value = 10;                
            return (value);
        }

        private bool ValidMoveOptimised(int[,] Board, StateofPlay GameState, Point start, Point finish, int AI)
        {

            //bool validmove = false;
            //int[,] ProposedPositions = new int[8, 8] { 
            //{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};

            DuplicateBoard(Board, ValidMovesPositions);
            StateofPlay SOP = DuplicateSOP(GameState);
            //try
            {
                //if ((finish.X < 9) && (finish.Y < 9) && (finish.X > 0) && (finish.Y > 0) && (start.X < 9) && (start.Y < 9) && (start.X > 0) && (start.Y > 0))
                {
                    //validmove = ValidMoveforPiece(ValidMovesPositions, ref SOP, start, finish); //Check to see if that piece can be moved (ignore discovered check for now)
                    //if (validmove)
                    if(ValidMoveforPiece(ValidMovesPositions, ref SOP, start, finish, AI)) //Check to see if that piece can be moved (ignore discovered check for now)
                    {   // Discovered check
                        MovePiece_Discovered_check(ValidMovesPositions, ref SOP, start, finish);//move the piece first
                        if (WhitePiece(finish, ValidMovesPositions))
                           // validmove = !InCheck(White, ValidMovesPositions, ref SOP); //check to see if we are now in check
                            return (!InCheck(White, ValidMovesPositions, ref SOP, AI)); //check to see if we are now in check
                        if (BlackPiece(finish, ValidMovesPositions))
                            // validmove = !InCheck(Black, ValidMovesPositions, ref SOP 
                            return (!InCheck(Black, ValidMovesPositions, ref SOP, AI));
                    }
                }
                //return validmove; 
                return (false);
            }
            //catch { MessageBox.Show("Problem with 'ValidMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            //return (false);
        }

        private bool ValidMove(int[,] Board, StateofPlay GameState, Point start, Point finish)
        {
            bool validmove = false;
            int[,] ProposedPositions = new int[8, 8] { 
            { 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0},{ 0,0,0,0,0,0,0,0}};

            DuplicateBoard(Board, ProposedPositions);
            StateofPlay SOP = DuplicateSOP(GameState);
            try
            {
                //if ((finish.X < 9) && (finish.Y < 9) && (finish.X > 0) && (finish.Y > 0) && (start.X < 9) && (start.Y < 9) && (start.X > 0) && (start.Y > 0))
                {
                    validmove = ValidMoveforPiece(ProposedPositions, ref SOP, start, finish, AI1); //Check to see if that piece can be moved (ignore discovered check for now)
                    if (validmove)
                    {   // Discovered check
                        MovePiece_Discovered_check(ProposedPositions, ref SOP, start, finish);//move the piece first
                        if (WhitePiece(finish, ProposedPositions))
                            validmove = !InCheck(White, ProposedPositions, ref SOP, AI1); //check to see if we are now in check
                        if (BlackPiece(finish, ProposedPositions))
                            validmove = !InCheck(Black, ProposedPositions, ref SOP, AI1);
                    }
                }
                return validmove;
            }
            catch { MessageBox.Show("Problem with 'ValidMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return validmove;
        }

        private void frmMChild_Chess_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width < this.Size.Height * 1.0)
            {
                sqwidth = (int) (this.Size.Width * 1.0 / 12);
                sqheight = (int) (this.Size.Width * 1.0 / 10);
            }
            else
            {
                sqwidth = (int) (this.Size.Height * 1.0 / 12);
                sqheight = (int) (this.Size.Height * 1.0 / 12);
            }
        }

        private void DuplicateBoard(int[,] original, int[,] duplicate)
        {
            Array.Copy(original, duplicate, 64);

            // for (int i = 0; i < 8; i++) ;
            /*
            for (int j = 0; j < 8; j++)
                duplicate[i, j] = original[i, j];*/
        }

        private bool ValidMoveforPiece(int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish, int AI)
        {
            int chesspiece = ProposedPositions[start.Y - 1, start.X - 1];
            bool valid = false;
            //if (!((finish.X < 9) && (finish.Y < 9) && (finish.X > 0) && (finish.Y > 0) && (start.X < 9) && (start.Y < 9) && (start.X > 0) && (start.Y > 0))) return false;
            //try
            {
                switch (chesspiece)
                {
                    case White_Pawn: valid = ValidPawnMove(White, ProposedPositions, ref SOP, start, finish); break;
                    case White_Bishop: valid = ValidBishopMove(White, ProposedPositions, ref SOP, start, finish); break;
                    case White_Rook: valid = ValidRookMove(White, ProposedPositions, ref SOP, start, finish); break;
                    case White_Knight: valid = ValidKnightMove(White, ProposedPositions, ref SOP, start, finish); break;
                    case White_King: valid = ValidKingMove(White, ProposedPositions, ref SOP, start, finish, AI); break;
                    case White_Queen: valid = ValidQueenMove(White, ProposedPositions, ref SOP, start, finish); break;
                    case Black_Pawn: valid = ValidPawnMove(Black, ProposedPositions, ref SOP, start, finish); break;
                    case Black_Bishop: valid = ValidBishopMove(Black, ProposedPositions, ref SOP, start, finish); break;
                    case Black_Rook: valid = ValidRookMove(Black, ProposedPositions, ref SOP, start, finish); break;
                    case Black_Knight: valid = ValidKnightMove(Black, ProposedPositions, ref SOP, start, finish); break;
                    case Black_King: valid = ValidKingMove(Black, ProposedPositions, ref SOP, start, finish, AI); break;
                    case Black_Queen: valid = ValidQueenMove(Black, ProposedPositions, ref SOP, start, finish); break;
                }
            }
            //catch { MessageBox.Show("Problem with 'ValidMoveoforPiece' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return (valid);
        }

        private bool ValidPawnMove(int Colour, int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish)
        {
            //try
            {
                if (Colour == White)
                {
                    if (finish.X == start.X)
                        if (finish.Y == start.Y + 1)
                            if (ProposedPositions[finish.Y - 1, finish.X - 1] == Empty)//Advance 1 square
                                return (true);
                    if (start.Y > 1)
                        if (BlackPiece(finish, ProposedPositions) &&   // Take a piece
                                ((finish.X == start.X + 1) || (finish.X == start.X - 1)) &&
                                (finish.Y == start.Y + 1))
                            return (true);
                    if (start.Y == 2)
                        if (finish.X == start.X)
                            if (finish.Y == start.Y + 2)
                                if ((ProposedPositions[2 + (int)start.Y - 1, (int)start.X - 1] == Empty) &&  //Advance 2 squares (for en Passant)
                                    (ProposedPositions[1 + (int)start.Y - 1, (int)start.X - 1] == Empty))
                                        return (true);
                    if (start.Y == 5)
                        if(finish.Y == 6) 
                           if(((SOP.blackPawns2stepColumn == start.X + 1) || (SOP.blackPawns2stepColumn == start.X - 1)) &&
                                (finish.X == SOP.blackPawns2stepColumn)) //En Passant opportunity taken
                                    return (true);
                }
                if (Colour == Black)
                {
                    if (finish.X == start.X)
                        if (finish.Y == start.Y - 1)
                            if (ProposedPositions[finish.Y - 1, finish.X - 1] == Empty)//Advance 1 square
                                return (true);
                    if (start.Y > 1)
                        if (WhitePiece(finish, ProposedPositions) &&   // Take a piece
                                ((finish.X == start.X + 1) || (finish.X == start.X - 1)) &&
                                (finish.Y == start.Y - 1))
                            return (true);
                    if (start.Y == 7)
                        if (finish.X == start.X)
                            if (finish.Y == start.Y - 2)
                                if ((ProposedPositions[-2 + (int)start.Y - 1, (int)start.X - 1] == Empty) &&  //Advance 2 squares (for en Passant)
                                    (ProposedPositions[-1 + (int)start.Y - 1, (int)start.X - 1] == Empty))
                                        return (true);
                    if (start.Y == 4)
                        if (finish.Y == 3)
                            if (((SOP.whitePawns2stepColumn == start.X + 1) || (SOP.whitePawns2stepColumn == start.X - 1)) &&
                                 (finish.X == SOP.whitePawns2stepColumn)) //En Passant opportunity taken
                                    return (true);
                }
            }
            //catch { MessageBox.Show("Problem with 'ValidPawnMove' routine" + start.ToString() + finish.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return (false);
        }

        private bool ValidKnightMove(int Colour, int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish)
        {
            //try
            {
                if (Colour == White)
                {
                    if (!WhitePiece(finish, ProposedPositions))
                    {
                        //if ((ProposedPositions[finish.Y - 1, finish.X - 1] == Empty) || BlackPiece(finish, ProposedPositions))
                        if ((((finish.X == start.X + 1) || (finish.X == start.X - 1)) && ((finish.Y == start.Y + 2) || (finish.Y == start.Y - 2))) ||
                            (((finish.Y == start.Y + 1) || (finish.Y == start.Y - 1)) && ((finish.X == start.X + 2) || (finish.X == start.X - 2))))
                            return (true);
                    }
                    else return false;
                }
                if (Colour == Black)
                {
                    if (!BlackPiece(finish, ProposedPositions))
                    {
                        //if ((ProposedPositions[finish.Y - 1, finish.X - 1] == Empty) || WhitePiece(finish, ProposedPositions))
                        if ((((finish.X == start.X + 1) || (finish.X == start.X - 1)) && ((finish.Y == start.Y + 2) || (finish.Y == start.Y - 2))) ||
                            (((finish.Y == start.Y + 1) || (finish.Y == start.Y - 1)) && ((finish.X == start.X + 2) || (finish.X == start.X - 2))))
                            return (true);
                    }

                }
            }
            //catch { MessageBox.Show("Problem with 'ValidKnightMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return false;
        }

        private bool ValidKingMove(int Colour, int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish, int AI)
        {            
            //try
            //if(AI1 == AI)
            {
                if (Colour == White)
                {
                    if ((ProposedPositions[finish.Y - 1, finish.X - 1] == Empty) || BlackPiece(finish, ProposedPositions))
                    if ((ProposedPositions[finish.Y - 1, finish.X - 1] == Empty) || BlackPiece(finish, ProposedPositions))
                        if (((finish.X == start.X + 1) || (finish.X == start.X - 1) || (finish.X == start.X)) &&
                            ((finish.Y == start.Y) || (finish.Y == start.Y + 1) || (finish.Y == start.Y - 1)))
                            return (true); // Normal 1 step move

                    if (!SOP.WhiteKingHasMoved) //start.X = 5
                    {
                        DuplicateBoard(ProposedPositions, CastlingPositions);
                        if (finish.X == 7)
                            if ((finish.Y == 1) && !SOP.WhiteKRookHasMovedOrTaken && (ProposedPositions[1 - 1, 6 - 1] == Empty) && (ProposedPositions[1 - 1, 7 - 1] == Empty))
                            {   // Kings side castle
                                if (InCheck(White, CastlingPositions, ref SOP, AI)) return (false); //Is king currently in check, if yes then cannot castle
                                CastlingPositions[1 - 1, 6 - 1] = White_King; CastlingPositions[1 - 1, 5 - 1] = Empty;
                                if (InCheck(White, CastlingPositions, ref SOP, AI)) return (false); //Is king walking through a check, if yes then cannot castle
                                return (true);
                            }
                        if (finish.X == 3)
                            if ((finish.Y == 1) && !SOP.WhiteQRookHasMovedOrTaken && (ProposedPositions[1 - 1, 4 - 1] == Empty) && (ProposedPositions[1 - 1, 3 - 1] == Empty) && (ProposedPositions[1 - 1, 2 - 1] == Empty))
                            {   // Queens side castle
                                if (InCheck(White, CastlingPositions, ref SOP, AI)) return (false);
                                CastlingPositions[1 - 1, 4 - 1] = White_King; CastlingPositions[1 - 1, 5 - 1] = Empty;
                                if (InCheck(White, CastlingPositions, ref SOP, AI)) return (false);
                                return (true);
                            }
                    }
                }
                if (Colour == Black)
                {
                    if ((ProposedPositions[finish.Y - 1, finish.X - 1] == Empty) || WhitePiece(finish, ProposedPositions))
                        if (((finish.X == start.X + 1) || (finish.X == start.X - 1) || (finish.X == start.X)) &&
                            ((finish.Y == start.Y) || (finish.Y == start.Y + 1) || (finish.Y == start.Y - 1)))
                            return (true);
                    if (!SOP.BlackKingHasMoved)
                    {
                        DuplicateBoard(ProposedPositions, CastlingPositions);
                        if (finish.X == 7)
                            if ((finish.Y == 8) && !SOP.BlackKRookHasMovedOrTaken && (ProposedPositions[8 - 1, 6 - 1] == Empty) && (ProposedPositions[8 - 1, 7 - 1] == Empty))
                            {
                                if (InCheck(Black, CastlingPositions, ref SOP, AI)) return (false);
                                CastlingPositions[8 - 1, 6 - 1] = Black_King; CastlingPositions[8 - 1, 5 - 1] = Empty;
                                if (InCheck(Black, CastlingPositions, ref SOP, AI)) return (false);
                                return (true);
                            }
                        if (finish.X == 3)
                            if ((finish.Y == 8) && !SOP.BlackQRookHasMovedOrTaken && (ProposedPositions[8 - 1, 4 - 1] == Empty) && (ProposedPositions[8 - 1, 3 - 1] == Empty) && (ProposedPositions[8 - 1, 2 - 1] == Empty))
                            {
                                if (InCheck(Black, CastlingPositions, ref SOP, AI)) return (false);
                                CastlingPositions[8 - 1, 4 - 1] = Black_King; CastlingPositions[8 - 1, 5 - 1] = Empty;
                                if (InCheck(Black, CastlingPositions, ref SOP, AI)) return (false);
                                return (true);
                            }
                    }
                }
            }


            //else

            //{
            //    if (Colour == White)
            //    {
            //        if ((ProposedPositions[finish.Y - 1, finish.X - 1] == Empty) || BlackPiece(finish, ProposedPositions))
            //            if (((finish.X == start.X + 1) || (finish.X == start.X - 1) || (finish.X == start.X)) &&
            //                ((finish.Y == start.Y) || (finish.Y == start.Y + 1) || (finish.Y == start.Y - 1)))
            //                return (true); // Normal 1 step move
            //        if (!SOP.WhiteKingHasMoved) //start.X = 5
            //        {
            //            if ((finish.X == 7) && (finish.Y == 1) && !SOP.WhiteKRookHasMovedOrTaken && (ProposedPositions[1 - 1, 6 - 1] == Empty) && (ProposedPositions[1 - 1, 7 - 1] == Empty))
            //            {   // Kings side castle
            //                if (!InCheck(White, CastlingPositions, ref SOP, AI)) return (true); //Is king currently in check, if yes then cannot castle
            //                CastlingPositions[1 - 1, 6 - 1] = White_King; CastlingPositions[1 - 1, 5 - 1] = Empty;
            //                if (!InCheck(White, CastlingPositions, ref SOP, AI)) return (true); //Is king walking through a check, if yes then cannot castle
            //            }
            //            if ((finish.X == 3) && (finish.Y == 1) && !SOP.WhiteQRookHasMovedOrTaken && (ProposedPositions[1 - 1, 4 - 1] == Empty) && (ProposedPositions[1 - 1, 3 - 1] == Empty) && (ProposedPositions[1 - 1, 2 - 1] == Empty))
            //            {   // Queens side castle
            //                if (!InCheck(White, CastlingPositions, ref SOP, AI)) return (true);
            //                CastlingPositions[1 - 1, 4 - 1] = White_King; CastlingPositions[1 - 1, 5 - 1] = Empty;
            //                if (!InCheck(White, CastlingPositions, ref SOP, AI)) return (true);
            //            }
            //        }
            //    }
            //    if (Colour == Black)
            //    {
            //        if ((ProposedPositions[finish.Y - 1, finish.X - 1] == Empty) || WhitePiece(finish, ProposedPositions))
            //            if (((finish.X == start.X + 1) || (finish.X == start.X - 1) || (finish.X == start.X)) &&
            //                ((finish.Y == start.Y) || (finish.Y == start.Y + 1) || (finish.Y == start.Y - 1)))
            //                return (true);
            //        if (!SOP.BlackKingHasMoved)
            //        {
            //            if ((finish.X == 7) && (finish.Y == 8) && !SOP.BlackKRookHasMovedOrTaken && (ProposedPositions[8 - 1, 6 - 1] == Empty) && (ProposedPositions[8 - 1, 7 - 1] == Empty))
            //            {
            //                if (!InCheck(Black, CastlingPositions, ref SOP, AI)) return (true);
            //                CastlingPositions[8 - 1, 6 - 1] = Black_King; CastlingPositions[8 - 1, 5 - 1] = Empty;
            //                if (!InCheck(Black, CastlingPositions, ref SOP, AI)) return (true);
            //            }
            //            if ((finish.X == 3) && (finish.Y == 8) && !SOP.BlackQRookHasMovedOrTaken && (ProposedPositions[8 - 1, 4 - 1] == Empty) && (ProposedPositions[8 - 1, 3 - 1] == Empty) && (ProposedPositions[8 - 1, 2 - 1] == Empty))
            //            {
            //                if (!InCheck(Black, CastlingPositions, ref SOP, AI)) return (true);
            //                CastlingPositions[8 - 1, 4 - 1] = Black_King; CastlingPositions[8 - 1, 5 - 1] = Empty;
            //                if (!InCheck(Black, CastlingPositions, ref SOP, AI)) return (true);
            //            }
            //        }
            //    }
            //}




            //catch { MessageBox.Show("Problem with 'ValidKingMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return false;
        }

        private bool ValidRookMove(int Colour, int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish)
        {
            //try
            {
                if (((Colour == White) && !WhitePiece(finish, ProposedPositions)) ||
                    ((Colour == Black) && !BlackPiece(finish, ProposedPositions)))
                {
                    if (start.Y == finish.Y)
                    {
                        if (start.X < finish.X)
                        {
                            for (int i = start.X + 1; i < finish.X; i++)
                                if (ProposedPositions[start.Y - 1, i - 1] != Empty)
                                    return(false);
                            return (true);
                        }
                        if (start.X > finish.X)
                        {
                            for (int i = start.X - 1; i > finish.X; i--)
                                if (ProposedPositions[start.Y - 1, i - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                    if (start.X == finish.X)
                    {
                        if (start.Y < finish.Y)
                        {
                            for (int i = start.Y + 1; i < finish.Y; i++)
                                if (ProposedPositions[i - 1, start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                        if (start.Y > finish.Y)
                        {
                            for (int i = start.Y - 1; i > finish.Y; i--)
                                if (ProposedPositions[i - 1, start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                }
                return false;
            }
            //catch { MessageBox.Show("Problem with 'ValidRookMove' routine" + start.ToString() + finish.ToString() + ProposedPositions.ToString(), "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            
        }

        private bool ValidBishopMove(int Colour, int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish)
        {
            //try
            {
                if (((Colour == White) && !WhitePiece(finish, ProposedPositions)) ||
                    ((Colour == Black) && !BlackPiece(finish, ProposedPositions)))
                {
                    if (start.Y - start.X == finish.Y - finish.X)
                    {   //    /
                        //   /
                        //  / 
                        if (start.X < finish.X)
                        {
                            for (int i = start.X + 1; i < finish.X; i++)
                                if (ProposedPositions[(i - start.X) + start.Y - 1, i - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                        if (start.X > finish.X)
                        {
                            for (int i = start.X - 1; i > finish.X; i--)
                                if (ProposedPositions[(i - start.X) + start.Y - 1, i - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                    if (start.Y + start.X == finish.Y + finish.X)
                    {   //  \
                        //   \
                        //    \
                        if (start.Y < finish.Y)
                        {
                            for (int i = start.Y + 1; i < finish.Y; i++)
                                if (ProposedPositions[i - 1, (-i + start.Y) + start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                        if (start.Y > finish.Y)
                        {
                            for (int i = start.Y - 1; i > finish.Y; i--)
                                if (ProposedPositions[i - 1, (-i + start.Y) + start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                }
                return false;
            }
            //catch { MessageBox.Show("Problem with 'ValidBishopMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            //return false;
        }

        private bool ValidQueenMove(int Colour, int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish)
        {
            //try
            {
                if (((Colour == White) && !WhitePiece(finish, ProposedPositions)) ||
                    ((Colour == Black) && !BlackPiece(finish, ProposedPositions)))
                {
                    if (start.Y == finish.Y)
                    {
                        if (start.X < finish.X)
                        {
                            for (int i = start.X + 1; i < finish.X; i++)
                                if (ProposedPositions[start.Y - 1, i - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                        if (start.X > finish.X)
                        {
                            for (int i = start.X - 1; i > finish.X; i--)
                                if (ProposedPositions[start.Y - 1, i - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                    if (start.X == finish.X)
                    {
                        if (start.Y < finish.Y)
                        {
                            for (int i = start.Y + 1; i < finish.Y; i++)
                                if (ProposedPositions[i - 1, start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                        if (start.Y > finish.Y)
                        {
                            for (int i = start.Y - 1; i > finish.Y; i--)
                                if (ProposedPositions[i - 1, start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                }
                if (((Colour == White) && !WhitePiece(finish, ProposedPositions)) ||
                    ((Colour == Black) && !BlackPiece(finish, ProposedPositions)))
                {
                    if (start.Y - start.X == finish.Y - finish.X)
                    {   //    /
                        //   /
                        //  / 
                        if (start.X < finish.X)
                        {
                            for (int i = start.X + 1; i < finish.X; i++)
                                if (ProposedPositions[(i - start.X) + start.Y - 1, i - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                        if (start.X > finish.X)
                        {
                            for (int i = start.X - 1; i > finish.X; i--)
                                if (ProposedPositions[(i - start.X) + start.Y - 1, i - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                    if (start.Y + start.X == finish.Y + finish.X)
                    {   //  \
                        //   \
                        //    \
                        if (start.Y < finish.Y)
                        {
                            for (int i = start.Y + 1; i < finish.Y; i++)
                                if (ProposedPositions[i - 1, (-i + start.Y) + start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                        if (start.Y > finish.Y)
                        {
                            for (int i = start.Y - 1; i > finish.Y; i--)
                                if (ProposedPositions[i - 1, (-i + start.Y) + start.X - 1] != Empty)
                                    return (false);
                            return (true);
                        }
                    }
                }
            }
            //catch { MessageBox.Show("Problem with 'ValidQueenMove' routine", "Logic Navigator failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            return false;
        }

        private bool InCheck(int Colour, int[,] ProposedPositions, ref StateofPlay SOP, int AI)
        {
            //Point BlackKing = new Point(0, 0);
            //if (Colour == Black) BlackKing = FindKing(Black, ProposedPositions);
            //Point WhiteKing = new Point(0, 0);
            //if (Colour == White) WhiteKing = FindKing(White, ProposedPositions);
            Point start = new Point(0, 0);
            if (Colour == White)
            {
                Point WhiteKing = FindKing(White, ProposedPositions);
                if (WhiteKing.X != 0)
                    for (int i = 1; i < 9; i++)
                        for (int j = 1; j < 9; j++)
                        {
                            start.X = i; start.Y = j;
                            if (BlackPiece(start, ProposedPositions))
                                if (ValidMoveforPiece(ProposedPositions, ref SOP, start, WhiteKing, AI))
                                    return (true);
                        }
                else return (false);
            }
            if (Colour == Black)
            {
                Point BlackKing = FindKing(Black, ProposedPositions);
                if (BlackKing.X != 0)
                    for (int i = 1; i < 9; i++)
                        for (int j = 1; j < 9; j++)
                        {
                            start.X = i; start.Y = j;
                            if (WhitePiece(start, ProposedPositions))
                                if (ValidMoveforPiece(ProposedPositions, ref SOP, start, BlackKing, AI))
                                    return (true);
                        }
                else return (false);
            }
            return (false);
        }

        private Point FindKing(int Colour, int[,] ProposedPositions)
        {
            Point king = new Point(0, 0);
            if(Colour == White)
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (ProposedPositions[i, j] == White_King)                           
                        {
                            king.X = j + 1; king.Y = i + 1;
                            return king;
                        }
            if(Colour == Black)
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if(ProposedPositions[i, j] == Black_King)
                        {
                            king.X = j + 1; king.Y = i + 1;
                            return king;
                        }
            return king;
        }

        private void MovePiece(int[,] Board, ref StateofPlay GameState, Point start, Point finish, bool allowadvancepawn, bool computercalc)
        {
            int piece = Board[start.Y - 1, start.X - 1];
            Board[finish.Y - 1, finish.X - 1] = piece;
            ////for (int k = 0; k < 300; k++)
            //{
            //    //if (checkBox1.Checked)
            //    {
                    if (piece == White_King)
                        if (start.X == 5)
                            if (start.Y == 1)
                                if (finish.Y == 1)
                                {
                                    if ((finish.X == 7))//gamestate.WhiteKingCastle)
                                        if (WhitePiece(start, Board))
                                        {
                                            Board[1 - 1, 6 - 1] = White_Rook;
                                            Board[1 - 1, 8 - 1] = Empty;
                                            //GameState.WhiteCastled = true;
                                        }
                                    if ((finish.X == 3))//(gamestate.WhiteQueenCastle)
                                        if (WhitePiece(start, Board))
                                        {
                                            Board[1 - 1, 4 - 1] = White_Rook;
                                            Board[1 - 1, 1 - 1] = Empty;
                                            //GameState.WhiteCastled = true;
                                        }
                                }
                    if (piece == Black_King)
                        if (start.X == 5)
                            if (start.Y == 8)
                                if (finish.Y == 8)
                                {
                                    if (finish.X == 7)//gamestate.BlackKingCastle)
                                        if (BlackPiece(start, Board))
                                        {
                                            Board[8 - 1, 6 - 1] = Black_Rook;
                                            Board[8 - 1, 8 - 1] = Empty;
                                            //GameState.BlackCastled = true;
                                        }
                                    if (finish.X == 3)//gamestate.BlackQueenCastle)
                                        if (BlackPiece(start, Board))
                                        {
                                            Board[8 - 1, 4 - 1] = Black_Rook;
                                            Board[8 - 1, 1 - 1] = Empty;
                                            //GameState.BlackCastled = true;
                                        }
                                }
            Board[start.Y - 1, start.X - 1] = Empty;
            if (((piece == White_Pawn) || (piece == Black_Pawn)) && allowadvancepawn)
            {
                if (finish.Y == 8) //Pawn makes it to the other side (White)
                {
                    if (computercalc)
                        Board[finish.Y - 1, finish.X - 1] = White_Queen;
                    else
                    {
                        Advance_Pawn Advance_pawn = new Advance_Pawn(White);
                        Advance_pawn.ShowDialog();
                        Board[finish.Y - 1, finish.X - 1] = Advance_pawn.piece;
                    }
                }
                if (finish.Y == 1) //Pawn makes it to the other side (Black)
                {
                    if (computercalc)
                        Board[finish.Y - 1, finish.X - 1] = Black_Queen;
                    else
                    {
                        Advance_Pawn Advance_pawn = new Advance_Pawn(Black);
                        Advance_pawn.ShowDialog();
                        Board[finish.Y - 1, finish.X - 1] = Advance_pawn.piece;
                    }
                }
            } 

            if (GameState.blackPawns2stepColumn > 0) //En Passant
            {
                if (((start.X == GameState.blackPawns2stepColumn + 1) || (start.X == GameState.blackPawns2stepColumn - 1)) &&
                    (finish.X == GameState.blackPawns2stepColumn) &&
                    (finish.Y == 6) && (start.Y == 5) && (piece == White_Pawn))
                    Board[-1 + finish.Y - 1, finish.X - 1] = Empty;
                else if (BlackPiece(finish, Board))
                    GameState.blackPawns2stepColumn = -1;
            }
            if (GameState.whitePawns2stepColumn > 0) //En Passant
            {
                if (((start.X == GameState.whitePawns2stepColumn + 1) || (start.X == GameState.whitePawns2stepColumn - 1)) &&
                    (finish.X == GameState.whitePawns2stepColumn) &&
                    (finish.Y == 3) && (start.Y == 4) && (piece == Black_Pawn))
                    Board[1 + finish.Y - 1, finish.X - 1] = Empty;
                else if (WhitePiece(finish, Board))
                    GameState.whitePawns2stepColumn = -1;
            }

            if (piece == White_Pawn) // pawn has moved two squares, for possible en passant
                if ((finish.Y == 4) && (start.Y == 2))
                    GameState.whitePawns2stepColumn = start.X;
            if (piece == Black_Pawn)
                if ((finish.Y == 5) && (start.Y == 7))
                    GameState.blackPawns2stepColumn = start.X;

            if (start.X == 1)
            {
                if (start.Y == 8) GameState.BlackQRookHasMovedOrTaken = true;
                if (start.Y == 1) GameState.WhiteQRookHasMovedOrTaken = true;
            }
            if (start.X == 8)
            {
                if (start.Y == 8) GameState.BlackKRookHasMovedOrTaken = true;
                if (start.Y == 1) GameState.WhiteKRookHasMovedOrTaken = true;
            }
            if (finish.X == 1)
            {
                if (finish.Y == 8) GameState.BlackQRookHasMovedOrTaken = true;
                if (finish.Y == 1) GameState.WhiteQRookHasMovedOrTaken = true;
            }
            if (finish.X == 8)
            {
                if (finish.Y == 8) GameState.BlackKRookHasMovedOrTaken = true;
                if (finish.Y == 1) GameState.WhiteKRookHasMovedOrTaken = true;
            }
            if (start.X == 5)
            {
                if (start.Y == 1) GameState.WhiteKingHasMoved = true;
                if (start.Y == 8) GameState.BlackKingHasMoved = true;
            }

            //Invalidate();
        }

        private void MovePiece_Discovered_check(int[,] ProposedPositions, ref StateofPlay SOP, Point start, Point finish)
        { // Move piece unconditionally. Used for checking to see if own king is discovered to be in check.
            int piece = ProposedPositions[start.Y - 1, start.X - 1];
            if ((start.X == finish.X + 1) || (start.X == finish.X - 1))
            {
                if ((finish.Y == 6) && (start.Y == 5) && (piece == White_Pawn) && (ProposedPositions[finish.Y - 1, finish.X - 1] == Empty)) //En Passant - white against black
                    ProposedPositions[-1 + finish.Y - 1, finish.X - 1] = Empty;
                if ((finish.Y == 3) && (start.Y == 4) && (piece == Black_Pawn) && (ProposedPositions[finish.Y - 1, finish.X - 1] == Empty)) //En Passant - black against white
                    ProposedPositions[1 + finish.Y - 1, finish.X - 1] = Empty;
            }
            ProposedPositions[finish.Y - 1, finish.X - 1] = piece;
            if (piece == White_King)
            {
                if ((start.X == 5) && (start.Y == 1) && (finish.X == 7) && (finish.Y == 1))//WhiteKingCastle
                    if (WhitePiece(start, ProposedPositions))
                    {
                        ProposedPositions[1 - 1, 6 - 1] = White_Rook;
                        ProposedPositions[1 - 1, 8 - 1] = Empty;
                    }
                if ((start.X == 5) && (start.Y == 1) && (finish.X == 3) && (finish.Y == 1))//WhiteQueenCastle
                    if (WhitePiece(start, ProposedPositions))
                    {
                        ProposedPositions[1 - 1, 4 - 1] = White_Rook;
                        ProposedPositions[1 - 1, 1 - 1] = Empty;
                    }
            }
            if (piece == Black_King)
            {
                if ((start.X == 5) && (start.Y == 8) && (finish.X == 7) && (finish.Y == 8))//BlackKingCastle
                    if (BlackPiece(start, ProposedPositions))
                    {
                        ProposedPositions[8 - 1, 6 - 1] = Black_Rook;
                        ProposedPositions[8 - 1, 8 - 1] = Empty;
                    }
                if ((start.X == 5) && (start.Y == 8) && (finish.X == 3) && (finish.Y == 8))//BlackQueenCastle
                    if (BlackPiece(start, ProposedPositions))
                    {
                        ProposedPositions[8 - 1, 4 - 1] = Black_Rook;
                        ProposedPositions[8 - 1, 1 - 1] = Empty;
                    }
            }
            ProposedPositions[start.Y - 1, start.X - 1] = Empty;
        }

        private StateofPlay InitialiseGameState()
        {
            StateofPlay beginning;
            beginning.whitePawns2stepColumn = -1;//for enpassant
            beginning.blackPawns2stepColumn = -1;
            beginning.BlackKingHasMoved = false; // For Castling
            beginning.BlackQRookHasMovedOrTaken = false;
            beginning.BlackKRookHasMovedOrTaken = false;
            //beginning.BlackCastled = false;
            beginning.WhiteKingHasMoved = false;
            beginning.WhiteQRookHasMovedOrTaken = false;
            beginning.WhiteKRookHasMovedOrTaken = false;
            //beginning.WhiteCastled = false;
            return beginning;
        }

        private StateofPlay DuplicateSOP(StateofPlay original)
        {
            //loopcount++;
            StateofPlay copy;
            copy.whitePawns2stepColumn = original.whitePawns2stepColumn;
            copy.blackPawns2stepColumn = original.blackPawns2stepColumn;
            copy.BlackKingHasMoved = original.BlackKingHasMoved; // For Castling
            copy.BlackQRookHasMovedOrTaken = original.BlackQRookHasMovedOrTaken;
            copy.BlackKRookHasMovedOrTaken = original.BlackKRookHasMovedOrTaken;
            copy.WhiteKingHasMoved = original.WhiteKingHasMoved;
            copy.WhiteQRookHasMovedOrTaken = original.WhiteQRookHasMovedOrTaken;
            copy.WhiteKRookHasMovedOrTaken = original.WhiteKRookHasMovedOrTaken;
            return copy;
        }

        private void AnalysisTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tm = AnalysisTreeView_AI1.SelectedNode;
            string move = tm.Text;

        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //int AI1 = White;
            bool even;
            if (autoplay)
                if (!moving)
                {
                    if((stalematecounter > 50) || checkmate)
                    {
                                        square.X = 8; square.Y = 8; clicked.X = 8; clicked.Y = 8;
                                        Array.Copy(CustomBoard, ChessPositions, 64);
                                        if (Math.Floor((double) games / 2) == (double) games / 2) even = true;
                                        else even = false;
                                        if (even)
                                        {
                                            label14.Text = "AI1 - White";
                                            label13.Text = "AI2 - Black";
                                        }
                                        else
                                        {
                                            label14.Text = "AI1 - Black";
                                            label13.Text = "AI2 - White";
                                        }
                                            
                                        if ((score != 100000) && (score != -100000))
                                        {                                            
                                            //if (winningamount == 0) winningamount = score;
                                            if (even)
                                                pointstally += winningamount;
                                            else pointstally -= winningamount;  // Positive favours the heuristics                                      
                                        }
                                        winningamount = 0;
                                        Gamesbox.Text = games.ToString();
                                        Moves.Text = ""; moverecord = ""; moverecordECO = "";
                                        Scorebox.Text = pointstally.ToString();
                                        if (stalematecounter > 25)
                                        {
                                            gameSummary.Text = gameSummary.Text + games.ToString() + ": Stalemate, " + movenumber.ToString() + ", " + Scorebox.Text; stalemates++;
                                            drawbox1.Text = stalemates.ToString();
                                            drawbox2.Text = stalemates.ToString(); 
                                        }
                                        if (checkmate)
                                        {
                                            if (winner == "Black")
                                            {
                                                gameSummary.Text = gameSummary.Text + games.ToString() + ": Black Wins";
                                                if (even)
                                                {
                                                    AI1blackwins++;
                                                    AI1blackbox.Text = AI1blackwins.ToString();
                                                }
                                                else
                                                {
                                                    AI2blackwins++;
                                                    AI2blackbox.Text = AI2blackwins.ToString();
                                                }
                                            }
                                            if (winner == "White")
                                            { 
                                                gameSummary.Text = gameSummary.Text + games.ToString() + ": White Wins";
                                                if (!even)
                                                {
                                                    AI1whitewins++;
                                                    AI1whitebox.Text = AI1whitewins.ToString();
                                                }
                                                else
                                                {
                                                    AI2whitewins++;
                                                    AI2whitebox.Text = AI2whitewins.ToString();
                                                }
                                            }           
                                            
                                        }
                                        gameSummary.Text = gameSummary.Text + ", " + movenumber.ToString() + "\r\n"; winner = "";
                                        games++; movenumber = 0; stalematecounter = 0; checkmate = false; endgame = false; middlegame = false; WhitePiecesTaken.Clear(); BlackPiecesTaken.Clear(); Scoregraph.Clear();
                                        GameBoard.Clear(); GameSOP.Clear();
                                        //MessageBox.Show("game finished", "Logic Navigator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                    }                    
                    ComputerPlay(-1);
                    //textBox2.Text = loopcount.ToString(); loopcount = 0;
                    textBox2.Text = ((float)loopcount2 / ((float)(loopcount + loopcount2))).ToString(); loopcount = 0; loopcount2 = 0;
                    Invalidate();
                }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (thinking)
                Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GameBoard.Count > 2)
            {
                DuplicateBoard((int[,])GameBoard[GameBoard.Count - 3], ChessPositions);
                StateofPlay gamestate = DuplicateSOP((StateofPlay)GameSOP[GameSOP.Count - 3]);
                GameBoard.RemoveRange(GameBoard.Count - 2, 2);
                GameSOP.RemoveRange(GameSOP.Count - 2, 2);
                RemoveLast2Moves();
                Invalidate();
            }
        }

        private void RemoveLast2Moves()
        {
            moverecordECO = moverecordECO.Substring(0, moverecordECO.Length - 10);
        }

        private void button2_Click(object sender, EventArgs e)
        { // AI1 vs AI2
            if (!autoplay) autoplay = true;
            else autoplay = false;
            square.X = 8; square.Y = 8; clicked.X = 8; clicked.Y = 8;
        }

        private void button3_Click(object sender, EventArgs e)
        { //Rotate Board            
                if (View == "White") View = "Black";
                else View = "White";
        }

        private void Randomise_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            square.X = 8; square.Y = 8; clicked.X = 8; clicked.Y = 8;
            if(((turn == White) && (BlackPlayer.Text.ToString() != "Human")) ||
               ((turn == Black) && (WhitePlayer.Text.ToString() != "Human")))
            //WhitePlayer.Text = "AI1";
            //BlackPlayer.Text = "Human";
            ComputerPlay(turn);
        }

        private void button4_TextChanged(object sender, EventArgs e)
        {

        }

        private void WhitePlayer_TextChanged(object sender, EventArgs e)
        {

        }

        private void FullAnalysis_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!autoplay) autoplay = true;
            else autoplay = false;
            square.X = 8; square.Y = 8; clicked.X = 8; clicked.Y = 8;  
        }

    }
    
    public struct StateofPlay
    {
        public int whitePawns2stepColumn;//column where last en passant occurred 
        public int blackPawns2stepColumn;
        public bool BlackKingHasMoved; // For Castling
        public bool BlackQRookHasMovedOrTaken;
        public bool BlackKRookHasMovedOrTaken;
        //public bool BlackCastled;
        public bool WhiteKingHasMoved;
        public bool WhiteQRookHasMovedOrTaken;
        public bool WhiteKRookHasMovedOrTaken;
        //public bool WhiteCastled;

    }

    public struct ECOCode
    {
        public string Name;
        public string moves;         
    }

    //e2e4 g8f6 e4e5 f6d5 c2c4 d5b6 d2d4 d7d5 c4c5 b6d7 b1c3 e7e6 g1f3 b8c6 f1d3 f7f6 e5f6 d7f6 c1f4 d8e7 c3b5 c8d7 b5c7 e8c8 
}

