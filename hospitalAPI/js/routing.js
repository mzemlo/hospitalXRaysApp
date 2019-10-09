var app = angular.module("hospitalApp", ["ngRoute"]);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "login-form.html",
            controller: 'SheetsControl'
        })
        .when("/home", {
            templateUrl: "start-page.html",
            controller: 'SheetsControl'
        })
        .when("/profile", {
            templateUrl: "profile.html",
            controller: 'SheetsControl'
        })
        .when("/edit-profile", {
            templateUrl: "profile-edit.html",
            controller: 'SheetsControl'
        })
        .when("/doctor-search", {
            templateUrl: "doctor-search.html",
            controller: 'SheetsControl'
        })
        .when("/doctor-add", {
            templateUrl: "doctor-add.html",
            controller: 'SheetsControl'
        })
        .when("/patient-search", {
            templateUrl: "patient-search.html",
            controller: 'SheetsControl'
        })
        .when("/patient-add", {
            templateUrl: "patient-add.html",
            controller: 'SheetsControl'
        })
        .when("/doctor/:docName", {
            templateUrl: 'doctor-page.html',
            controller: 'SheetsControl'
        })
        .when("/edit-doctor/:docName", {
            templateUrl: 'doctor-edit.html',
            controller: 'SheetsControl'
        })
        .when("/patient/:patName", {
            templateUrl: 'patient-page.html',
            controller: 'SheetsControl'
        })
        .when("/edit-patient/:patName", {
            templateUrl: 'patient-edit.html',
            controller: 'SheetsControl'
        })
        .when("/photo/:photoName", {
            templateUrl: 'photo-page.html',
            controller: 'SheetsControl'
        })
        .otherwise({
            templateUrl: "start-page.html"
        });
});




