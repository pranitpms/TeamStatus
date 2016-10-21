(function() {

    angular.module('TeamStatus')
        .controller('UiviewController', function () {

            var vm = this;

            vm.items = [
                { code: 1, description: 'pranit' },
                { code: 2, description: 'nikhil' },
                { code: 3, description: 'hrishikesh' },
                { code: 4, description: 'ram' },
                { code: 5, description: 'rajesh' },
                { code: 6, description: 'girish' }
            ];

            vm.value = {};

            vm.onSelect = function (model) {
                Console.log(model);
            };
        });
})();