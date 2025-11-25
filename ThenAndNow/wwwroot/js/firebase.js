firebase.initializeApp({
    apiKey: "AIzaSyDXvvPWVSvKQAr4sLfYskstSKLRYfRsSaw",
    authDomain: "poznandawniejdzis-79c46.firebaseapp.com",
    databaseURL: "https://poznandawniejdzis-79c46-default-rtdb.firebaseio.com",
    projectId: "poznandawniejdzis-79c46",
    storageBucket: "poznandawniejdzis-79c46.firebasestorage.app",
    messagingSenderId: "1033991145858",
    appId: "1:1033991145858:web:28d914c9b8fc24ef88679b",
    measurementId: "G-MZRRG17H7D"
});

const database = firebase.database();

window.firebaseInterop = {
    // Rating
    getRatingById: async function (refPath) {
        try {
            const snapshot = await database.ref(refPath).once("value");
            const data = snapshot.val();
            return data || { a: 0, b: 0 };
        } catch (error) {
            console.error("firebaseInterop.getRatingById error: ", error);
            return { a: 0, b: 0 };
        }
    },

    updateRating: async function (refPath, rating) {
        try {
            await database.ref(refPath).set({
                a: rating.a,
                b: rating.b,
            });
            return true;
        } catch (error) {
            console.error("firebaseInterop.updateRating error: ", error);
            return false;
        }
    },

    // Replies
    addReply: async function (refPath, reply) {
        try {
            await database.ref(refPath).push(reply)
            return true;
        } catch (error) {
            console.error("firebaseInterop.addReply error:", error);
            return false;
        }
    },

    getRepliesById: async function (refPath) {
        try {
            const snapshot = await database.ref(refPath).once("value");
            const data = snapshot.val();
            return Object.values(data ?? {});
        } catch (error) {
            console.error("firebaseInterop.getRepliesById error: ", error);
            return [];
        }
    },
};
