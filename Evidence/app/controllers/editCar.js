angular.module("myApp")
    .controller("editCarCtrl", ($scope, editUrl, carSvc, $routeParams) => {
        $scope.current = {};
        var id = $routeParams.id;
        var old = $scope.model.cars.find(c => c.Id == id);
        angular.copy(old, $scope.current);
        $scope.update = f => {

            var obj =
            {
                Id: $scope.current.Id,
                Company: $scope.current.Company,
                Model: $scope.current.Model,
                ReleaseDate: $scope.current.ReleaseDate,
                Price: $scope.current.Price,
                Picture: ""
            }
            if ($scope.current.Picture.filesize && $scope.current.Picture.filesize >= 0) {
                obj.Picture = $scope.current.Picture.base64;
                obj.Imagetype = $scope.current.Picture.filetype;
            }
            carSvc.put(editUrl + "/" + $scope.current.Id, obj)
                .then(r => {

                    var i = $scope.model.cars.findIndex(c => c.Id == id);
                    $scope.model.cars[i].Company = r.data.Company;
                    $scope.model.cars[i].Model = r.data.Model;
                    $scope.model.cars[i].ReleaseDate = r.data.ReleaseDate;
                    $scope.model.cars[i].Price = r.data.Price;
                    $scope.model.cars[i].Picture = r.data.Picture;
                    $scope.apiMessage = "Succesfully modified data.";
                    f.$setPristine();
                    f.$setUntouched();
                }, err => {
                    $scope.apiMessage = "Failed to modified data.";
                })

        }
    })