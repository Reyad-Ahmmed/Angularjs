angular.module("myApp")
    .controller("createCarCtrl", ($scope, createUrl, carSvc) => {
        $scope.current = {};
        $scope.insert = f => {

            carSvc.post(createUrl, {
                Company: $scope.current.Company,
                Model: $scope.current.Model,
                ReleaseDate: $scope.current.ReleaseDate,
                Price: $scope.current.Price,
                Picture: $scope.current.Picture.base64,
                Imagetype: $scope.current.Picture.filetype
            })
            .then(r => {
                $scope.model.cars.push(r.data);
                $scope.apiMessage = "Successfully save data."
                $scope.current = {};
                f.$setPristine();
                f.$setUntouched();
            }, err => {
                $scope.apiMessage = "Failed to save data in database."
            })


        }

    })