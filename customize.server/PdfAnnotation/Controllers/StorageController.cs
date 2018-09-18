using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http;

namespace PdfAnnoation.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AnnotationController : ControllerBase {

        private readonly StorageService storage;
        private readonly ILogger<AnnotationController> logger;

        public AnnotationController(StorageService storage, ILogger<AnnotationController> logger) {
            this.storage = storage;
            this.logger = logger;
        }

        [HttpPost]
        public dynamic GetAnnotations(GetAnnotationsReq req) {
            return storage.GetAnnotations(req.Document, req.Page);
        }

        [HttpPost]
        public dynamic GetAnnotation(GetAnnotationReq req) {
            return storage.GetAnnotation(req.Document, req.Uuid);
        }

        [HttpPost]
        public dynamic AddAnnotation(AnnotationInfo req) {
            req.Uuid = Guid.NewGuid().ToString("N");
            return storage.AddAnnotation(req);
        }

        [HttpPost]
        public dynamic EditAnnotation(AnnotationInfo req) {
            return storage.EditAnnotation(req.Document, req.Uuid, req);
        }

        [HttpPost]
        public bool DeleteAnnotation(DeleteAnnotationReq req) {
            return storage.DeleteAnnotation(req.Document, req.Uuid);
        }

        [HttpPost]
        public List<CommentInfo> GetComments(GetCommentReq req) {
            logger.LogInformation($"Get comments {req.Document} {req.Annotation}");
            return storage.GetComments(req.Document, req.Annotation);
        }

        [HttpPost]
        public CommentInfo AddComment(AddCommentReq req) {
            logger.LogInformation($"Add comment {req.Document} {req.Annotation} {req.Content}");
            var comment = new CommentInfo {
                Class = "Comment",
                Uuid = Guid.NewGuid().ToString("N"),
                Annotation = req.Annotation,
                Content = req.Content,
                Document = req.Document
            };

            return storage.AddComment(comment);
        }

        [HttpPost]
        public bool DeleteComment(DeleteCommentReq req) {
            return storage.DeleteComment(req.Document, req.Uuid);
        }
    }
}