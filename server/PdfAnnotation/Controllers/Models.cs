namespace PdfAnnoation.Controllers {

    public class Rectangle {
        public double Height { set; get; }
        public double Width { set; get; }
        public double X { set; get; }
        public double Y { set; get; }
    }

    public class AnnotationInfo {
        // Common
        public string Class { set; get; }
        public string Type { set; get; }
        public int Page { set; get; }
        public string Uuid { set; get; }

        // Area
        public string Color { set; get; }
        public Rectangle[] Rectangles { set; get; }

        // Drawing
        public double Width { set; get; }
        public double[][] Lines { set; get; }

        // Point
        public double X { set; get; }
        public double Y { set; get; }

        // Text
        public double Size { set; get; }
        public string Content { set; get; }
        public double Height { set; get; }

        // Extra
        public string Document { set; get; }
    }

    public class CommentInfo {
        public string Class { set; get; }
        public string Type { set; get; }
        public string Annotation { set; get; }
        public string Content { set; get; }
        public string Uuid { set; get; }

        // Extra
        public string Document { set; get; }
    }
}