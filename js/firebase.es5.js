"use strict";

firebase.initializeApp({
    apiKey: "AIzaSyDXvvPWVSvKQAr4sLfYskstSKLRYfRsSaw",
    authDomain: "poznandawniejdzis-79c46.firebaseapp.com",
    databaseURL: "https://poznandawniejdzis-79c46-default-rtdb.firebaseio.com",
    projectId: "poznandawniejdzis-79c46",
    storageBucket: "poznandawniejdzis-79c46.appspot.com",
    messagingSenderId: "1033991145858",
    appId: "1:1033991145858:web:28d914c9b8fc24ef88679b",
    measurementId: "G-MZRRG17H7D"
});

var database = firebase.database();

window.firebaseInterop = {
    // Entries
    getDetailsById: function getDetailsById(refPath) {
        var snapshot, data;
        return regeneratorRuntime.async(function getDetailsById$(context$1$0) {
            while (1) switch (context$1$0.prev = context$1$0.next) {
                case 0:
                    context$1$0.prev = 0;
                    context$1$0.next = 3;
                    return regeneratorRuntime.awrap(database.ref(refPath).once("value"));

                case 3:
                    snapshot = context$1$0.sent;
                    data = snapshot.val();
                    return context$1$0.abrupt("return", data || { a: null });

                case 8:
                    context$1$0.prev = 8;
                    context$1$0.t0 = context$1$0["catch"](0);

                    console.error("firebaseInterop.getDetailsById error: ", context$1$0.t0);
                    return context$1$0.abrupt("return", { a: null });

                case 12:
                case "end":
                    return context$1$0.stop();
            }
        }, null, this, [[0, 8]]);
    },

    // Rating
    getRatingById: function getRatingById(refPath) {
        var snapshot, data;
        return regeneratorRuntime.async(function getRatingById$(context$1$0) {
            while (1) switch (context$1$0.prev = context$1$0.next) {
                case 0:
                    context$1$0.prev = 0;
                    context$1$0.next = 3;
                    return regeneratorRuntime.awrap(database.ref(refPath).once("value"));

                case 3:
                    snapshot = context$1$0.sent;
                    data = snapshot.val();
                    return context$1$0.abrupt("return", data || { a: 0, b: 0 });

                case 8:
                    context$1$0.prev = 8;
                    context$1$0.t0 = context$1$0["catch"](0);

                    console.error("firebaseInterop.getRatingById error: ", context$1$0.t0);
                    return context$1$0.abrupt("return", { a: 0, b: 0 });

                case 12:
                case "end":
                    return context$1$0.stop();
            }
        }, null, this, [[0, 8]]);
    },

    updateRating: function updateRating(refPath, rating) {
        return regeneratorRuntime.async(function updateRating$(context$1$0) {
            while (1) switch (context$1$0.prev = context$1$0.next) {
                case 0:
                    context$1$0.prev = 0;
                    context$1$0.next = 3;
                    return regeneratorRuntime.awrap(database.ref(refPath).set({
                        a: rating.a,
                        b: rating.b
                    }));

                case 3:
                    return context$1$0.abrupt("return", true);

                case 6:
                    context$1$0.prev = 6;
                    context$1$0.t0 = context$1$0["catch"](0);

                    console.error("firebaseInterop.updateRating error: ", context$1$0.t0);
                    return context$1$0.abrupt("return", false);

                case 10:
                case "end":
                    return context$1$0.stop();
            }
        }, null, this, [[0, 6]]);
    },

    // Replies
    addReply: function addReply(refPath, reply) {
        var replyRef;
        return regeneratorRuntime.async(function addReply$(context$1$0) {
            while (1) switch (context$1$0.prev = context$1$0.next) {
                case 0:
                    context$1$0.prev = 0;
                    replyRef = push(ref(database, refPath));

                    set(reply);
                    return context$1$0.abrupt("return", true);

                case 6:
                    context$1$0.prev = 6;
                    context$1$0.t0 = context$1$0["catch"](0);

                    console.error("firebaseInterop.addReply error: ", context$1$0.t0);
                    return context$1$0.abrupt("return", false);

                case 10:
                case "end":
                    return context$1$0.stop();
            }
        }, null, this, [[0, 6]]);
    },

    getRepliesById: function getRepliesById(refPath) {
        var snapshot, data;
        return regeneratorRuntime.async(function getRepliesById$(context$1$0) {
            while (1) switch (context$1$0.prev = context$1$0.next) {
                case 0:
                    context$1$0.prev = 0;
                    context$1$0.next = 3;
                    return regeneratorRuntime.awrap(database.ref(refPath).once("value"));

                case 3:
                    snapshot = context$1$0.sent;
                    data = snapshot.val();
                    return context$1$0.abrupt("return", data || []);

                case 8:
                    context$1$0.prev = 8;
                    context$1$0.t0 = context$1$0["catch"](0);

                    console.error("firebaseInterop.getRepliesById error: ", context$1$0.t0);
                    return context$1$0.abrupt("return", []);

                case 12:
                case "end":
                    return context$1$0.stop();
            }
        }, null, this, [[0, 8]]);
    }
};

