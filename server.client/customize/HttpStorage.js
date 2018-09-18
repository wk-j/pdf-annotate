import pdfjs from "../../";
import axios from "axios";

let { StoreAdapter } = pdfjs;

export class Http2 extends StoreAdapter {
    constructor() {

        var root = "/api/annotation";

        super({
            getAnnotations(documentId, pageNumber) {
                console.log("get annotations");
                var url = `${root}/getAnnotations`;
                return new Promise((ok, err) => {
                    axios.post(url, {
                        document: documentId,
                        page: pageNumber
                    }).then(res => {
                        ok(res.data);
                    })
                });
            },

            getAnnotation(documentId, annotationId) {
                var url = `${root}/getAnnotation`;
                return new Promise((resolve, reject) => {
                    axios.post(url, {
                        document: documentId,
                        uuid: annotationId
                    }).then(rs => {
                        resolve(rs.data);
                    });
                });
            },

            addAnnotation(documentId, pageNumber, annotation) {
                var url = `${root}/addAnnotation`;
                return new Promise((resolve, reject) => {
                    annotation.page = pageNumber;
                    annotation.document = documentId
                    axios.post(url, annotation).then(rs => {
                        resolve(rs.data);
                    })
                });
            },

            editAnnotation(documentId, annotationId, annotation) {
                console.log("edit annotation");
                var url = `${root}/editAnnotation`;
                return new Promise((ok, err) => {
                    annotation.document = documentId;
                    annotation.uuid = annotationId;
                    axios.post(url, annotation).then(rs => {
                        ok(rs.data);
                    })
                });
            },

            deleteAnnotation(documentId, annotationId) {
                var url = `${root}/deleteAnnotation`;
                return new Promise((ok, err) => {
                    axios.post(url, {
                        uuid: annotationId,
                        document: documentId
                    }).then(rs => {
                        ok(rs.data);
                    });
                });
            },

            getComments(documentId, annotationId) {
                var url = `${root}/getComments`;
                return new Promise((ok, err) => {
                    axios.post(url, {
                        document: documentId,
                        annotation: annotationId
                    }).then(rs => {
                        ok(rs.data);
                    })
                });
            },

            addComment(documentId, annotationId, content) {
                var url = `${root}/addComment`;
                return new Promise((ok, err) => {
                    var comment = {
                        document: documentId,
                        annotation: annotationId,
                        content: content
                    }
                    axios.post(url, comment).then(rs => {
                        ok(rs.data);
                    })
                });
            },

            deleteComment(documentId, commentId) {
                var url = `${root}/deleteComment`;
                return new Promise((ok, err) => {
                    axios.post(url, {
                        document: documentId,
                        uuid: commentId
                    })
                });
            }
        });
    }
}

// StoreAdapter for working with localStorage
// This is ideal for testing, examples, and prototyping
export class HttpStorageAdapter extends StoreAdapter {
    constructor() {
        super({
            getAnnotations(documentId, pageNumber) {
                return new Promise((resolve, reject) => {
                    let annotations = getAnnotations(documentId).filter((i) => {
                        return i.page === pageNumber && i.class === 'Annotation';
                    });

                    resolve({
                        documentId,
                        pageNumber,
                        annotations
                    });
                });
            },

            getAnnotation(documentId, annotationId) {
                return Promise.resolve(getAnnotations(documentId)[findAnnotation(documentId, annotationId)]);
            },

            addAnnotation(documentId, pageNumber, annotation) {
                return new Promise((resolve, reject) => {
                    annotation.class = 'Annotation';
                    annotation.uuid = uuid();
                    annotation.page = pageNumber;

                    let annotations = getAnnotations(documentId);
                    annotations.push(annotation);
                    updateAnnotations(documentId, annotations);

                    resolve(annotation);
                });
            },

            editAnnotation(documentId, annotationId, annotation) {
                return new Promise((resolve, reject) => {
                    let annotations = getAnnotations(documentId);
                    annotations[findAnnotation(documentId, annotationId)] = annotation;
                    updateAnnotations(documentId, annotations);

                    resolve(annotation);
                });
            },

            deleteAnnotation(documentId, annotationId) {
                return new Promise((resolve, reject) => {
                    let index = findAnnotation(documentId, annotationId);
                    if (index > -1) {
                        let annotations = getAnnotations(documentId);
                        annotations.splice(index, 1);
                        updateAnnotations(documentId, annotations);
                    }

                    resolve(true);
                });
            },

            getComments(documentId, annotationId) {
                return new Promise((resolve, reject) => {
                    resolve(getAnnotations(documentId).filter((i) => {
                        return i.class === 'Comment' && i.annotation === annotationId;
                    }));
                });
            },

            addComment(documentId, annotationId, content) {
                return new Promise((resolve, reject) => {
                    let comment = {
                        class: 'Comment',
                        uuid: uuid(),
                        annotation: annotationId,
                        content: content
                    };

                    let annotations = getAnnotations(documentId);
                    annotations.push(comment);
                    updateAnnotations(documentId, annotations);

                    resolve(comment);
                });
            },

            deleteComment(documentId, commentId) {
                return new Promise((resolve, reject) => {
                    getAnnotations(documentId);
                    let index = -1;
                    let annotations = getAnnotations(documentId);
                    for (let i = 0, l = annotations.length; i < l; i++) {
                        if (annotations[i].uuid === commentId) {
                            index = i;
                            break;
                        }
                    }

                    if (index > -1) {
                        annotations.splice(index, 1);
                        updateAnnotations(documentId, annotations);
                    }

                    resolve(true);
                });
            }
        });
    }
}

function getAnnotations(documentId) {
    return JSON.parse(localStorage.getItem(`${documentId}/annotations`)) || [];
}

function updateAnnotations(documentId, annotations) {
    localStorage.setItem(`${documentId}/annotations`, JSON.stringify(annotations));
}

function findAnnotation(documentId, annotationId) {
    let index = -1;
    let annotations = getAnnotations(documentId);
    for (let i = 0, l = annotations.length; i < l; i++) {
        if (annotations[i].uuid === annotationId) {
            index = i;
            break;
        }
    }
    return index;
}