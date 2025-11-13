import { initializeApp }
    from "https://www.gstatic.com/firebasejs/12.3.0/firebase-app.js";

import { getAuth, GoogleAuthProvider, signInWithPopup, getRedirectResult, signOut }
    from "https://www.gstatic.com/firebasejs/12.3.0/firebase-auth.js";

import { getDatabase, ref, get, set, push }
    from "https://www.gstatic.com/firebasejs/12.3.0/firebase-database.js";

const firebaseConfig = {
    apiKey: "AIzaSyDXvvPWVSvKQAr4sLfYskstSKLRYfRsSaw",
    authDomain: "poznandawniejdzis-79c46.firebaseapp.com",
    databaseURL: "https://poznandawniejdzis-79c46-default-rtdb.firebaseio.com",
    projectId: "poznandawniejdzis-79c46",
    storageBucket: "poznandawniejdzis-79c46.appspot.com",
    messagingSenderId: "1033991145858",
    appId: "1:1033991145858:web:28d914c9b8fc24ef88679b",
    measurementId: "G-MZRRG17H7D",
};

const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
const database = getDatabase(app);

window.firebaseInterop = {
    // Auth
    getCurrentUser: () => auth.currentUser,

    signInWithGoogle: async () => {
        const provider = new GoogleAuthProvider();
        try {
            // Start redirect flow
            await signInWithPopup(auth, provider);
            return { status: "redirecting" };
        } catch (error) {
            console.error("firebaseInterop.signInWithGoogle error: ", error);
            return { error: error.message };
        }
    },

    getRedirectResult: async () => {
        try {
            const result = await getRedirectResult(auth);
            if (result && result.user) {
                return {
                    name: result.user.displayName,
                    email: result.user.email,
                    token: await result.user.getIdToken()
                };
            }
            return null;
        } catch (error) {
            console.error("firebaseInterop.getRedirectResult error: ", error);
            return { error: error.message };
        }
    },

    signOut: async () => {
        await signOut(auth);
    },

    // Rating
    getRatingById: async (refPath) => {
        try {
            const snapshot = await get(ref(database, refPath));
            return snapshot.val() || { a: 0, b: 0 };
        } catch (error) {
            console.error("firebaseInterop.getRatingById error: ", error);
            return { a: 0, b: 0 };
        }
    },

    updateRating: async (refPath, rating) => {
        try {
            await set(ref(database, refPath), { a: rating.a, b: rating.b });
            return true;
        } catch (error) {
            console.error("firebaseInterop.updateRating error: ", error);
            return false;
        }
    },

    // Replies
    addReply: async (refPath, reply) => {
        try {
            await push(ref(database, refPath), reply);
            return true;
        } catch (error) {
            console.error("firebaseInterop.addReply error:", error);
            return false;
        }
    },

    getRepliesById: async (refPath) => {
        try {
            const snapshot = await get(ref(database, refPath));
            return Object.values(snapshot.val() ?? {});
        } catch (error) {
            console.error("firebaseInterop.getRepliesById error: ", error);
            return [];
        }
    }
};
