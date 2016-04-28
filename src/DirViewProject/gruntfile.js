module.exports = function (grunt) {
    grunt.initConfig({
        concat: {
            js: {
                src: ['Scripts/DirView/Services/DirViewService.js', 'Scripts/DirView/app.js', 'Scripts/DirView/Controllers/DirViewController.js'],
                dest: 'wwwroot/app.js',
            }
        },
        uglify: {
            build: {
                files: {
                    'wwwroot/bundles/angular.js': ['wwwroot/lib/angular/angular.js'],
                    'wwwroot/bundles/angular-resource.js': ['wwwroot/lib/angular-resource/angular-resource.js'],
                    'wwwroot/bundles/jquery.js': ['wwwroot/lib/jquery/dist/jquery.js'],
                    'wwwroot/bundles/jqueryval.js': ['wwwroot/lib/jquery-validation/dist/jquery.validate.js', 'lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js'],
                    'wwwroot/bundles/bootstrap.js': ['wwwroot/lib/bootstrap/dist/js/bootstrap.min.js'],
                    'wwwroot/bundles/app.js': ['wwwroot/app.js']
                }
            }
        },
        clean: ["wwwroot/app.js"],
  
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');

    grunt.registerTask('build', ['clean', 'concat', 'uglify']);
};