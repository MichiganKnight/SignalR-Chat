const AuthStorage = {
    saveUser: function(user) {
        localStorage.setItem("currentUser", JSON.stringify(user));
    },
    
    getUser: function() {
        const user = localStorage.getItem("currentUser");
        
        return user ? JSON.parse(user) : null;
    },
    
    clearUser: function() {
        localStorage.removeItem("currentUser");   
    }
};