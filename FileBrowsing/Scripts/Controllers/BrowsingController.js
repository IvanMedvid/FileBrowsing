browsingApp.controller('BrowsingController',
    function BrowsingController($scope, $http) {
        $scope.parent = false;
        $scope.downloadProgress = true;
        $http({ method: 'GET', url: '/api/Data' }).
            then(function(data, status, headers, config) {
                
                $scope.browsingInfo = data.data;
                if ($scope.browsingInfo.currentDirectory === null) {
                    $scope.browsingInfo.currentDirectory = "";
                }
                if ($scope.browsingInfo.parentDirectory !== null) {
                    $scope.parent = true;
                }
                $scope.downloadProgress = false;
            }, function(data, status, headers, config) {
                $scope.downloadProgress = false;
                console.log(status);
            });

        $scope.load = function(path) {
            $scope.downloadProgress = true;
            var fullPath = "";
            var currentPath = $scope.browsingInfo.currentDirectory;
            if ($scope.browsingInfo.parentDirectory === path) {
                var partsOfPath = currentPath.split("\\");
                for (var i = 0; i < partsOfPath.length - 1; i++) {
                    if (i !== (partsOfPath.length - 2)) {
                        fullPath += partsOfPath[i] + "\\";
                    } else {
                        fullPath += partsOfPath[i];
                    }

                }
            } else if (currentPath.lastIndexOf("\\") === (currentPath.length - 1)) {
                fullPath = currentPath + path;
            } else if (currentPath === "") {
                fullPath = path;
            } else {
                fullPath = currentPath + "\\" + path;
            }
            $http({ method: 'GET', url: '/api/Data' + "?path=" + fullPath }).
                then(function(data, status, headers, config) {
                    $scope.downloadProgress = false;
                    $scope.browsingInfo = data.data;
                    if ($scope.browsingInfo.parentDirectory !== null) {
                        $scope.parent = true;
                    } else {
                        $scope.parent = false;
                    }
                }, function(data, status, headers, config) {
                    $scope.downloadProgress = false;
                    console.log(status);
                });
        }

    })