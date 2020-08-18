module.exports = function (grunt) {
	const getBuildContainerParams = () => {
		return {
			options: { stdout: true },
			command: `powershell -NonInteractive -ExecutionPolicy Bypass Source\\Docker\\container-local.ps1 -operation "build" < NUL`
		}
	}

	const getPushContainerParams = () => {
		return {
			options: { stdout: true },
			command: `powershell -NonInteractive -ExecutionPolicy Bypass Source\\Docker\\container-local.ps1 -operation "push" < NUL`
		}
	}

	var solution = "Source/ElementsCloud.Tests.sln";

	// Params:
	// configuration: Compile in release or debug mode.
	// publishProfile: The Release publish profile will exclude customer specific files. Empty string (no profile) will keep files "as is".
	// withPackage: True will build deploy-package, false not.
	var getMsbuild = function (targets, configuration) {
		return {
			src: [solution],
			options: {
				projectConfiguration: configuration,
				targets: targets,
				maxCpuCount: 4,
				buildParameters: (function () {
					return {
						Platform: "Any CPU",
						WarningLevel: 2
					};
				}()),
				inferMsbuildPath: true,
				verbosity: "Minimal" //Quiet, Minimal, Normal, Detailed, Diagnostic
			}
		}
	};

    var getTest = function (mode, framework, testCaseFilter) {

        var patterns = ["Source/**/*.Test.dll", "Source/**/*.Tests.dll", "!**/obj/**"];

        if (framework == "core") {
            patterns.push("!**/net471/**")
        } else {
            patterns.push("!**/netcoreapp2.2/**")
        }

		return {
            src: patterns,
			options: {
				/* Sample filter:
				"TestCategory=NAR|Priority=1"
				"Owner=vikram&TestCategory!=UI"
				"FullyQualifiedName~NameSpace.Class"
				"(TestCategory!=UI&(Priority=1|Priority=2))|(TestCategory=UI&Priority=1)"
				*/
				testCaseFilter: testCaseFilter,
				platform: "x64",
				inIsolation: true,
                logger: "trx;LogFileName=TestResultVSTest-" + framework + ".trx;verbosity=minimal",
			}
		}
	};

	var getContinuousTests = function (mode, framework) {
		return getTest(mode, framework, "TestCategory!=KnownError");
	};


	var getSingleTest = function (mode, testName) {
		if (typeof mode === "undefined" || typeof testName === "undefined") {
			return;
		}
		return getTest(mode, "FullyQualifiedName~" + testName);
	}

	grunt.initConfig({
		pkg: grunt.file.readJSON("package.json"),
		msbuild: {
			release: getMsbuild(["Clean", "Rebuild"], "Release"),
			debug: getMsbuild(["Clean", "Rebuild"], "Debug"),
			clean: getMsbuild(["Clean"])
		},
		vstest: {
            allTestsFull: getContinuousTests("Release", "4x"),
            allTestsCore: getContinuousTests("Release", "core"),
			singleTest: getSingleTest(grunt.option("mode"), grunt.option("test"))
		},
		nugetrestore: {
			project: {
				src: solution,
				dest: "Dependencies/packages",
				options: {
					configFile: "Source/nuget.config"
				}
			},
		},
		cleanfiles: {
			options: {
				folders: true,
				force: true
			},
			src: [
				"TestResults/",
				"Source/**/bin/**",
				"Source/**/obj/**",
				"Source/Docker/content/**",
				"Source/Chocolatey/Artifacts/*.*",
				"dist/"
			],
			folder: ['C:\\ephorte\\']
		},
		shell: {
			clearNuget: {
				options: { stdout: true },
				command: ".\\node_modules\\grunt-nuget\\libs\\nuget.exe locals all -clear "
			},
			buildContainer_Project: getBuildContainerParams(),
			pushContainer_Project: getPushContainerParams()
		},
		copy: {
			dist: {
				files: [
					{
						expand: true,
						cwd: "Documentation",
						src: ["**"],
						dest: "dist/"
					},
					{
						expand: true,
						cwd: "Source",
						src: ["**/bin/release/**", "!**/obj/**"],
						dest: "dist/Tests"
					}
				]
			},
		}
	});

    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-msbuild");
	grunt.loadNpmTasks("grunt-vstest");
	grunt.loadNpmTasks("grunt-nuget");
	grunt.loadNpmTasks("grunt-contrib-clean");
	grunt.loadNpmTasks("grunt-shell");
	grunt.loadNpmTasks('grunt-force-task');

	grunt.renameTask("clean", "cleanfiles"); //Rename so that task-name "clean" can be used as "main" clean

	// Default task(s).
	grunt.registerTask("clearNuget", ["shell:clearNuget"]);
    grunt.registerTask("build", ["nugetrestore:project", "msbuild:release"]);
    grunt.registerTask("test", ["vstest:allTestsFull", "vstest:allTestsCore"]);
	grunt.registerTask("clean", ["msbuild:clean", "cleanfiles"]);
	grunt.registerTask("runContainerScripts", ["shell:runContainerScripts"]);
	grunt.registerTask("removeContainerImages", ["shell:removeOracleImages"]);
	grunt.registerTask("dist", ["copy:dist"]);
    grunt.registerTask("default", ["force:clean", "build", /*"test",*/ "dist"]);
	grunt.registerTask("defaultFullClean", ["clearNuget", "default"]);
};
