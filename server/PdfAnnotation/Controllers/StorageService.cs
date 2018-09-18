using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace PdfAnnoation.Controllers {

    public class StorageService {

        private readonly List<AnnotationInfo> ann = new List<AnnotationInfo>();
        private readonly List<CommentInfo> cmm = new List<CommentInfo>();

        private readonly ILogger<StorageService> logger;

        public StorageService(ILogger<StorageService> logger) {
            this.logger = logger;
        }

        public dynamic GetAnnotations(string documentId, int pageNumber) {
            return new {
                DocumentId = documentId,
                PageNumber = pageNumber,
                Annotations = ann.Where(x => x.Document == documentId && x.Page == pageNumber)
            };
        }

        public AnnotationInfo GetAnnotation(string documentId, string annotationId) {
            return ann.Where(x => x.Document == documentId && x.Uuid == annotationId).FirstOrDefault();
        }

        public AnnotationInfo AddAnnotation(AnnotationInfo annotation) {
            ann.Add(annotation);
            return annotation;
        }

        public AnnotationInfo EditAnnotation(string documentId, string annotationId, AnnotationInfo annotation) {
            var index = ann.FindIndex(x => x.Uuid == annotationId && x.Document == documentId);
            ann[index] = annotation;
            return annotation;
        }

        public bool DeleteAnnotation(string documentId, string annotationId) {
            var index = ann.FindIndex(x => x.Document == documentId && x.Uuid == annotationId);
            ann.RemoveAt(index);
            return true;
        }

        public CommentInfo AddComment(CommentInfo info) {
            cmm.Add(info);
            return info;
        }

        public List<CommentInfo> GetComments(string documentId, string annotationId) {
            logger.LogInformation("Get comments from {0}", cmm.Count);
            return cmm.Where(x => x.Document == documentId && x.Annotation == annotationId).ToList();
        }

        public bool DeleteComment(string documentId, string commentId) {
            var index = cmm.FindIndex(x => x.Uuid == commentId);
            cmm.RemoveAt(index);
            return true;
        }
    }
}