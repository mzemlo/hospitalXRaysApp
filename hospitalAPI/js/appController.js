app.controller("SheetsControl", function ($scope, $routeParams) {
    patFilter();
    docFilter();
    $scope.doctors = modelDoctors;
    for (var i = 0; i < modelDoctors.length; i++) {
        if (modelDoctors[i].Id == $routeParams.docName) {
            $scope.doc_name = i;
        }
    }
    
    $scope.patients = modelPatients;
    for (var i = 0; i < modelPatients.length; i++) {
        $scope.patients[i].x = "";
        if (modelPatients[i].NoFlat != null && modelPatients[i].NoFlat != "") {
            $scope.patients[i].x = "/";                     
        }
        if (modelPatients[i].Id == $routeParams.patName) {
            $scope.pat_name = i;
        }
    }
    $scope.Photos = modelPhotos;
    for (var i = 0; i < modelPhotos.length; i++) {
        if (modelPhotos[i].Id == $routeParams.photoName) {
            $scope.photo_name = i;
        }
    }
    $scope.curUser =
        {
            Id: curUserId,
            Role: curUserRole,
            Name: curUserName,
            Surname: curUserSurname,
            PESEL: curUserPesel,
            Contract: curUserContract,
            License: curUserLicense,
            Email: curUserEmail
        }
    $scope.doctorAccess = isNotDoctor;
    $scope.adminAccess = isNotAdmin;
    $scope.loggedAccess = isLogged;
    $scope.onlyUnloggedAccess = true;
    if (isLogged) {
        $scope.onlyUnloggedAccess = false;
    } else {
        $scope.onlyUnloggedAccess = true;
    }
    if (!isLogged) {
        $scope.doctorAccess = false;
        $scope.adminAccess = false;
    }

});

app.controller("MenuControl", function ($scope, $routeParams) {
    $scope.Options = modelOptions;

    //if (curUserRole == "Admin") {
    //    $scope.isNotDoctor = true;
    //} else {
    //    $scope.isNotDoctor = false;
    //}
});