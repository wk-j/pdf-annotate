namespace PdfAnnoation.Controllers {
    public class GetAnnotationsReq {
        public string Document { set; get; }
        public int Page { set; get; }
    }

    public class GetAnnotationReq {
        public string Uuid { set; get; }
        public string Document { set; get; }
    }

    public class DeleteAnnotationReq {
        public string Document { set; get; }
        public string Uuid { set; get; }
    }

    public class GetCommentReq {
        public string Document { set; get; }
        public string Annotation { set; get; }
    }

    public class AddCommentReq {
        public string Document { set; get; }
        public string Annotation { set; get; }
        public string Content { set; get; }
    }

    public class DeleteCommentReq {
        public string Document { set; get; }
        public string Uuid { set; get; }
    }
}