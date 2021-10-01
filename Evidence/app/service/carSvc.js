angular.module("myApp")
    .factory("carSvc", ($http) => {
        return {
            get: u => {
                return $http({
                    method: "GET",
                    url: u
                })
            },
            post: (u, d) => {
                return $http({
                    method: "POST",
                    url: u,
                    data: d
                })
            },
            put: (u, d) => {
                return $http({
                    method: "PUT",
                    url: u,
                    data: d
                })
            },
            delete: u => {
                return $http({
                    method: "DELETE",
                    url: u
                })
            }
        }
    })