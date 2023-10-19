using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using PZ1.Enums;
using PZ1.Helpers;
using PZ1.Model;
using PZ1.Windows;

namespace PZ1
{
    public struct Pointt
    {
        public double x;
        public double y;
        public Pointt(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum LineType
        {
            Verical, Horizontal,
        }

        public double X;

        public double Y;

        public double xMax { get; set; }

        public double yMin { get; set; }

        public double xMin { get; set; }

        public double yMax { get; set; }

        public ShapeOption? SelectedShape { get; set; } = null;

        public static Ellipse ellipse = null;

        public static Polygon polygon = null;

        public static TextBox text = null;

        public static TextBlock shapeText = null;

        public XmlEntities xmlEntities { get; set; } = ParseXml();

        public const double minLongitude = 19.727275;

        public const double maxLongitude = 19.950944;

        public const double minLatitude = 45.189725;

        public const double maxLatitude = 45.328735;

        public bool WasCleared { get; set; }

        private static Dictionary<long, Model.Point> collectionOfNodes = new Dictionary<long, Model.Point>();

        private static Dictionary<Pointt, LineType> horizontalLineOnPoint = new Dictionary<Pointt, LineType>();

        private static Dictionary<Pointt, LineType> verticalLineOnPoint = new Dictionary<Pointt, LineType>();

        public static List<System.Windows.Point> polygonPoints = new List<System.Windows.Point>();

        public static Dictionary<UIElement, TextBlock> ShapeTextPairs = new Dictionary<UIElement, TextBlock>();

        public static Dictionary<string, bool> points = new Dictionary<string, bool>();

        public static Dictionary<long, System.Windows.Point> entityIds = new Dictionary<long, System.Windows.Point>();

        public static List<UIElement> UndoHistory { get; set; } = new List<UIElement>();

        public static List<UIElement> CanvasObjects { get; set; } = new List<UIElement>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private static Line createLine(Model.Point point1, Model.Point point2)
        {
            Line line = new Line();
            line.Stroke = new SolidColorBrush(Colors.Gold);

            line.X1 = point1.X + 4.5;
            line.Y1 = point1.Y + 4.5;
            line.X2 = point2.X + 4.5;
            line.Y2 = point2.Y + 4.5;

            line.StrokeThickness = 1.5;

            return line;
        }

        private static Rectangle CreateRectangle(Color color)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = 9;
            rectangle.Width = 9;
            rectangle.Fill = new SolidColorBrush(color);
            return rectangle;
        }

        private static Model.Point CreatePoint(double longitude, double latitude)
        {
            double ValueoOfOneLongitude = (maxLongitude - minLongitude) / 2750; //pravimo 2200 dijelova (Longituda) jer nam je canvas 2200x2200 
            double ValueoOfOneLatitude = (maxLatitude - minLatitude) / 2750;

            double x = Math.Round((longitude - minLongitude) / ValueoOfOneLongitude); // koliko longituda stane u rastojanje izmedju trenutne i minimalne longitude
            double y = Math.Round((maxLatitude - latitude) / ValueoOfOneLatitude);

            x = x - x % 10; // zaokruzi na prvi broj dijeljiv sa 10, toliko ce nam biti rastojanje izmedju dva susjedna x
            y = y - y % 10; // zaokruzi na prvi broj dijeljiv sa 10, toliko ce nam biti rastojanje izmedju dva susjedna y

            int cout = 0;

            while (true)
            {
                if (collectionOfNodes.Any(z => z.Value.X == x && z.Value.Y == y))
                {
                    if (cout == 0)
                    {
                        x += 10;
                        cout++;
                        continue;
                    }
                    else if (cout == 1)
                    {
                        x -= 20;
                        cout++;
                        continue;
                    }
                    else if (cout == 2)
                    {
                        x += 10; //vracamo x na pocetnu vrijednost
                        y += 10;
                        cout++;
                        continue;
                    }
                    else if (cout == 3)
                    {
                        y -= 20;
                        cout++;
                        continue;
                    }
                    else if (cout == 4)
                    {
                        x += 10;
                        cout++;
                        continue;
                    }
                    else if (cout == 5)
                    {
                        x -= 20;
                        cout++;
                        continue;
                    }
                    else if (cout == 6)
                    {
                        y += 20;
                        cout++;
                        continue;
                    }
                    else if (cout == 6)
                    {
                        x += 20;
                        cout++;
                        continue;
                    }
                    else
                    {
                        cout = 0;
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
            return new Model.Point
            {
                X = x,
                Y = y,
            };
        }

        public static List<Rectangle> DrawSubstations(List<SubstationEntity> substations)
        {
            List<Rectangle> substationRectangles = new List<Rectangle>();
            foreach (var item in substations)
            {
                if (collectionOfNodes.ContainsKey(item.Id))
                {
                    continue;
                }
                Rectangle rectangle = CreateRectangle(Colors.LightPink);

                var point = CreatePoint(item.Longitude, item.Latitude);

                rectangle.SetValue(Canvas.LeftProperty, point.X);
                rectangle.SetValue(Canvas.TopProperty, point.Y);
                rectangle.ToolTip = "\tSUBSTATION\nID: " + item.Id.ToString() + "\nName:" + item.Name;

                substationRectangles.Add(rectangle);
                collectionOfNodes.Add(item.Id, point);
            }
            return substationRectangles;
        }

        public static List<Rectangle> DrawNodes(List<NodeEntity> nodes)
        {
            List<Rectangle> nodeRectangles = new List<Rectangle>();
            foreach (var item in nodes)
            {
                if (collectionOfNodes.ContainsKey(item.Id))
                {
                    continue;
                }
                Rectangle rectangle = CreateRectangle(Colors.BlueViolet);

                var point = CreatePoint(item.Longitude, item.Latitude);

                rectangle.SetValue(Canvas.LeftProperty, point.X);
                rectangle.SetValue(Canvas.TopProperty, point.Y);
                rectangle.ToolTip = "\tNODE\nID: " + item.Id.ToString() + "\nName: " + item.Name;

                collectionOfNodes.Add(item.Id, point);
                nodeRectangles.Add(rectangle);
            }
            return nodeRectangles;
        }

        public static List<Rectangle> DrawSwitch(List<SwitchEntity> switches)
        {
            List<Rectangle> switchRectangles = new List<Rectangle>();
            foreach (var item in switches)
            {
                if (collectionOfNodes.ContainsKey(item.Id))
                {
                    continue;
                }
                Rectangle rectangle = CreateRectangle(Colors.LightGreen);

                var point = CreatePoint(item.Longitude, item.Latitude);

                rectangle.SetValue(Canvas.LeftProperty, point.X);
                rectangle.SetValue(Canvas.TopProperty, point.Y);
                rectangle.ToolTip = "\tSWITCH \nID: " + item.Id.ToString() + "\nName: " + item.Name + "\nStatus: " + item.Status;

                collectionOfNodes.Add(item.Id, point);
                switchRectangles.Add(rectangle);
            }
            return switchRectangles;
        }

        public static void DrawLines(List<Model.LineEntity> lines, Canvas canvas)
        {
            Model.Point startNode = new Model.Point();
            Model.Point EndNode = new Model.Point();

            Model.Point currPoint = new Model.Point();
            Model.Point prevPoint = new Model.Point();

            foreach (var item in lines)
            {
                if (!collectionOfNodes.ContainsKey(item.FirstEnd) || !collectionOfNodes.ContainsKey(item.SecondEnd))
                {
                    continue;
                }
                startNode = collectionOfNodes[item.FirstEnd];
                EndNode = collectionOfNodes[item.SecondEnd];

                prevPoint.X = currPoint.X = startNode.X;
                prevPoint.Y = currPoint.Y = startNode.Y;

                int step = (currPoint.X > EndNode.X) ? -10 : 10; // razmak izmedju dva x-a na gridu je 10, zbog toga i korak -10 ili +10

                while (currPoint.X != EndNode.X) //crtamo po x
                {
                    currPoint.X += step;
                    if (!horizontalLineOnPoint.ContainsKey(new Pointt(currPoint.X, currPoint.Y)) || currPoint.X == EndNode.X)
                    {
                        Line l1 = createLine(prevPoint, currPoint);
                        l1.ToolTip = "\tLINE \nID: " + item.Id.ToString() + "\nName: " + item.Name + "\nFirstEnd: " + item.FirstEnd.ToString() + "\nSecondEnd: " + item.SecondEnd.ToString();
                        canvas.Children.Add(l1);
                        horizontalLineOnPoint[new Pointt(currPoint.X, currPoint.Y)] = LineType.Horizontal;
                    }
                    prevPoint.X = currPoint.X;
                }
                step = (currPoint.Y > EndNode.Y) ? -10 : 10; // razmak izmedju dva y-a na gridu je 10, zbog toga i korak -10 ili +10
                while (currPoint.Y != EndNode.Y) //crtamo po y
                {
                    currPoint.Y += step;
                    if (!verticalLineOnPoint.ContainsKey(new Pointt(currPoint.X, currPoint.Y)) || currPoint.Y == EndNode.Y)
                    {
                        Line l1 = createLine(prevPoint, currPoint);
                        l1.ToolTip = "\tLINE \nID: " + item.Id.ToString() + "\nName: " + item.Name + "\nFirstEnd: " + item.FirstEnd.ToString() + "\nSecondEnd: " + item.SecondEnd.ToString();
                        canvas.Children.Add(l1);
                        verticalLineOnPoint[new Pointt(currPoint.X, currPoint.Y)] = LineType.Verical;
                    }
                    prevPoint.Y = currPoint.Y;
                }
            }
        }


        private void LoadModel_Click(object sender, RoutedEventArgs e)
        {
            List<Rectangle> rectangles = new List<Rectangle>();

            rectangles = DrawSubstations(xmlEntities.Substations);
            foreach (var item in rectangles)
            {
                canvas.Children.Add(item);
            }

            rectangles = DrawNodes(xmlEntities.Nodes);
            foreach (var item in rectangles)
            {
                canvas.Children.Add(item);
            }

            rectangles = DrawSwitch(xmlEntities.Switches);
            foreach (var item in rectangles)
            {
                canvas.Children.Add(item);
            }

            DrawLines(xmlEntities.Lines, canvas);

            //Oznacavanje presjeka vodova ->
            foreach (var point in horizontalLineOnPoint.Keys)
            {
                int step = 10;
                if (verticalLineOnPoint.ContainsKey(new Pointt(point.x, point.y)))
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Height = 5;
                    rectangle.Width = 5;
                    rectangle.Fill = new SolidColorBrush(Color.FromRgb(66, 65, 11));
                    rectangle.SetValue(Canvas.LeftProperty, point.x + 3);
                    rectangle.SetValue(Canvas.TopProperty, point.y + 3);
                    canvas.Children.Add(rectangle);
                }
            }

            btnLoadModel.IsEnabled = false;
        }

        private void Shape_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as Button;

            if (clickedButton == null)
            {
                return;
            }

            MakeButtonGreen(clickedButton.Name);
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (WasCleared)
            {
                foreach (UIElement element in UndoHistory)
                {
                    if (ShapeTextPairs.ContainsKey(element))
                    {
                        canvas.Children.Add(ShapeTextPairs[element]);
                    }
                    canvas.Children.Add(element);
                    CanvasObjects.Add(element);
                }

                UndoHistory.Clear();
                WasCleared = false;

                return;
            }

            if (canvas.Children.Count > 0)
            {
                var element = canvas.Children[canvas.Children.Count - 1];
                UndoHistory.Add(element);
                CanvasObjects.Remove(element);
                canvas.Children.Remove(element);
                if (ShapeTextPairs.ContainsKey(element))
                {
                    canvas.Children.Remove(ShapeTextPairs[element]);
                }
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (UndoHistory.Count > 0 && WasCleared == false)
            {
                var element = UndoHistory[UndoHistory.Count - 1];
                if (ShapeTextPairs.ContainsKey(element))
                {
                    canvas.Children.Add(ShapeTextPairs[element]);
                }
                canvas.Children.Add(element);
                CanvasObjects.Add(element);
                UndoHistory.RemoveAt(UndoHistory.Count - 1);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in CanvasObjects)
            {
                UndoHistory.Add(element);
                canvas.Children.Remove(element);
                if (ShapeTextPairs.ContainsKey(element))
                {
                    canvas.Children.Remove(ShapeTextPairs[element]);
                }
            }

            CanvasObjects.Clear();
            WasCleared = true;
        }


        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (SelectedShape)
            {
                case ShapeOption.Ellipse:
                    // Create ellipse
                    ellipse = null;
                    var ellipsePosition = Mouse.GetPosition(canvas);
                    var ellipseWindow = new EllipseWindow(ellipsePosition);

                    ellipseWindow.ShowDialog();

                    // Check for ellipse
                    if (ellipse == null)
                    {
                        SelectedShape = null;
                        MakeButtonsNormal();
                        return;
                    }

                    // Check for and draw text in ellipse
                    if (shapeText != null)
                    {
                        canvas.Children.Add(shapeText);
                        ShapeTextPairs[ellipse] = shapeText;
                        Panel.SetZIndex(shapeText, 1);
                        shapeText = null;
                    }

                    // Draw ellipse
                    ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
                    canvas.Children.Add(ellipse);

                    // Undo/Redo/Clear Commands
                    CanvasObjects.Add(ellipse);
                    UndoHistory.Clear();
                    polygonPoints.Clear();
                    ellipse = null;
                    SelectedShape = null;
                    MakeButtonsNormal();
                    break;
                case ShapeOption.Polygon:
                    var position = Mouse.GetPosition(canvas);
                    polygonPoints.Add(position);
                    break;
                case ShapeOption.Text:
                    // Create ellipse
                    var textPosition = Mouse.GetPosition(canvas);
                    var textWindow = new TextWindow(textPosition);

                    textWindow.ShowDialog();

                    // Check for ellipse
                    if (text == null)
                    {
                        SelectedShape = null;
                        MakeButtonsNormal();
                        return;
                    }

                    // Draw ellipse
                    text.PreviewMouseLeftButtonDown += Text_PreviewMouseLeftButtonDown;
                    canvas.Children.Add(text);

                    // Undo/Redo/Clear Commands
                    CanvasObjects.Add(text);
                    UndoHistory.Clear();
                    polygonPoints.Clear();
                    text = null;
                    SelectedShape = null;
                    MakeButtonsNormal();
                    break;
            }
        }

        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedShape != ShapeOption.Polygon)
            {
                return;
            }

            // Exit the method if the list is null or empty
            if (polygonPoints == null || polygonPoints.Count < 3)
            {
                return;
            }

            // Create polygon
            var polygonWindow = new PolygonWindow(polygonPoints);
            polygonWindow.ShowDialog();

            // Check for polygon
            if (polygon == null)
            {
                SelectedShape = null;
                MakeButtonsNormal();
                return;
            }

            // Check for and draw text in polygon
            if (shapeText != null)
            {
                canvas.Children.Add(shapeText);
                ShapeTextPairs[polygon] = shapeText;
                Panel.SetZIndex(shapeText, 1);
                shapeText = null;
            }

            // Draw polygon
            polygon.MouseLeftButtonDown += Polygon_MouseLeftButtonDown;
            canvas.Children.Add(polygon);

            // Undo/Redo/Clear Commands
            CanvasObjects.Add(polygon);
            UndoHistory.Clear();
            polygonPoints.Clear();

            polygon = null;
            SelectedShape = null;
            MakeButtonsNormal();
        }

        public void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedEllipse = (Ellipse)sender;
            TextBlock textBlock;
            ShapeTextPairs.TryGetValue(clickedEllipse, out textBlock);

            var ellipseWindow = new EllipseWindow(clickedEllipse, textBlock);
            ellipseWindow.ShowDialog();

            if (ellipse == null)
            {
                return;
            }

            ellipse.Margin = clickedEllipse.Margin;

            // First draw text if there is any
            if (shapeText != null)
            {
                GeneralHelper.AdjustMarginToCenter(shapeText, ellipse);
                canvas.Children.Remove(textBlock);
                canvas.Children.Add(shapeText);
                ShapeTextPairs.Remove(clickedEllipse);
                ShapeTextPairs[ellipse] = shapeText;
                Panel.SetZIndex(shapeText, 1);
            }

            ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            canvas.Children.Remove(clickedEllipse);
            canvas.Children.Add(ellipse);

            var index = CanvasObjects.IndexOf(clickedEllipse);
            CanvasObjects.RemoveAt(index);
            CanvasObjects.Insert(index, ellipse);

            ellipse = null;
            shapeText = null;
        }

        public void Polygon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedPolygon = (Polygon)sender;
            TextBlock textBlock;
            ShapeTextPairs.TryGetValue(clickedPolygon, out textBlock);

            var polygonWindow = new PolygonWindow(clickedPolygon, textBlock);
            polygonWindow.ShowDialog();

            if (polygon == null)
            {
                return;
            }

            polygon.Margin = clickedPolygon.Margin;

            // First draw text if there is any
            if (shapeText != null)
            {
                GeneralHelper.AdjustMarginToCenter(shapeText, polygon);
                canvas.Children.Remove(textBlock);
                canvas.Children.Add(shapeText);
                ShapeTextPairs.Remove(clickedPolygon);
                ShapeTextPairs[polygon] = shapeText;
                Panel.SetZIndex(shapeText, 1);
            }

            polygon.MouseLeftButtonDown += Polygon_MouseLeftButtonDown;
            canvas.Children.Remove(clickedPolygon);
            canvas.Children.Add(polygon);

            var index = CanvasObjects.IndexOf(clickedPolygon);
            CanvasObjects.RemoveAt(index);
            CanvasObjects.Insert(index, polygon);

            polygon = null;
            shapeText = null;
        }

        public void Text_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedText = (TextBox)sender;
            var textWindow = new TextWindow(clickedText);
            textWindow.ShowDialog();

            if (text == null)
            {
                return;
            }

            text.Margin = clickedText.Margin;
            text.PreviewMouseLeftButtonDown += Text_PreviewMouseLeftButtonDown;
            canvas.Children.Remove(clickedText);
            canvas.Children.Add(text);

            var index = CanvasObjects.IndexOf(clickedText);
            CanvasObjects.RemoveAt(index);
            CanvasObjects.Insert(index, text);
        }

        private void MakeButtonsNormal()
        {
            foreach (var button in buttonGrid.Children.OfType<Button>())
            {
                button.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void MakeButtonGreen(string buttonName)
        {
            foreach (var button in buttonGrid.Children.OfType<Button>())
            {
                // Unselect other buttons
                if (button.Name != buttonName)
                {
                    button.Background = new SolidColorBrush(Colors.LightGray);
                    continue;
                }

                // Toggle selection for pressed button
                if (button.Background is SolidColorBrush solidColorBrush && solidColorBrush.Color == Colors.LightGreen)
                {
                    button.Background = new SolidColorBrush(Colors.LightGray);
                    SelectedShape = null;
                }
                else
                {
                    button.Background = new SolidColorBrush(Colors.LightGreen);
                    if (buttonName == "btnEllipse" || buttonName == "btnPolygon" || buttonName == "btnText")
                    {
                        SelectedShape = (ShapeOption)Enum.Parse(typeof(ShapeOption), buttonName.Substring(3));
                    }
                    polygonPoints.Clear();
                }
            }
        }

        public static XmlEntities ParseXml()
        {
            var filename = "Geographic.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var purchaseOrderFilepath = System.IO.Path.Combine(currentDirectory, filename);

            StringBuilder result = new StringBuilder();

            //Load xml
            XDocument xdoc = XDocument.Load(filename);

            //Run query
            var substations = xdoc.Descendants("SubstationEntity")
                     .Select(sub => new SubstationEntity
                     {
                         Id = (long)sub.Element("Id"),
                         Name = (string)sub.Element("Name"),
                         X = (double)sub.Element("X"),
                         Y = (double)sub.Element("Y"),
                     }).ToList();

            double longit = 0;
            double latid = 0;

            foreach (var item in substations)
            {
                ToLatLon(item.X, item.Y, 34, out latid, out longit);
                item.Latitude = latid;
                item.Longitude = longit;
            }

            var nodes = xdoc.Descendants("NodeEntity")
                     .Select(node => new NodeEntity
                     {
                         Id = (long)node.Element("Id"),
                         Name = (string)node.Element("Name"),
                         X = (double)node.Element("X"),
                         Y = (double)node.Element("Y"),
                     }).ToList();

            foreach (var item in nodes)
            {
                ToLatLon(item.X, item.Y, 34, out latid, out longit);
                item.Latitude = latid;
                item.Longitude = longit;
            }

            var switches = xdoc.Descendants("SwitchEntity")
                     .Select(sw => new SwitchEntity
                     {
                         Id = (long)sw.Element("Id"),
                         Name = (string)sw.Element("Name"),
                         Status = (string)sw.Element("Status"),
                         X = (double)sw.Element("X"),
                         Y = (double)sw.Element("Y"),
                     }).ToList();

            foreach (var item in switches)
            {
                ToLatLon(item.X, item.Y, 34, out latid, out longit);
                item.Latitude = latid;
                item.Longitude = longit;
            }

            var lines = xdoc.Descendants("LineEntity")
                     .Select(line => new LineEntity
                     {
                         Id = (long)line.Element("Id"),
                         Name = (string)line.Element("Name"),
                         ConductorMaterial = (string)line.Element("ConductorMaterial"),
                         IsUnderground = (bool)line.Element("IsUnderground"),
                         R = (float)line.Element("R"),
                         FirstEnd = (long)line.Element("FirstEnd"),
                         SecondEnd = (long)line.Element("SecondEnd"),
                         LineType = (string)line.Element("LineType"),
                         ThermalConstantHeat = (long)line.Element("ThermalConstantHeat"),
                         Vertices = line.Element("Vertices").Descendants("Point").Select(p => new Model.Point
                         {
                             X = (double)p.Element("X"),
                             Y = (double)p.Element("Y"),
                         }).ToList()
                     }).ToList();

            foreach (var line in lines)
            {
                foreach (var point in line.Vertices)
                {
                    ToLatLon(point.X, point.Y, 34, out latid, out longit);
                    point.Latitude = latid;
                    point.Longitude = longit;
                }
            }
            return new XmlEntities { Substations = substations, Switches = switches, Nodes = nodes, Lines = lines };
        }

        public static void ToLatLon(double utmX, double utmY, int zoneUTM, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = true;

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = zoneUTM;
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }
    }
}