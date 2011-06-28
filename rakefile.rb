require 'albacore'

MSBUILD_PATH = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/"
BUILD_PATH = File.expand_path('build')
TOOLS_PATH = File.expand_path('tools')
REPORTS_PATH = File.expand_path('reports')
ARTIFACTS_PATH = File.expand_path('artifacts')
LIB_PATH = File.expand_path('lib')
SOLUTION = "src/ExpectedObjects.sln"
COMPILE_TARGET = "Release"
nuget = 'nuget'
load "VERSION.txt"

task :default => ["build:all"]

namespace :build do

	task :all => [:clean, :versioning, :compile, :tests, :package]

	assemblyinfo :versioning do |asm|
  	asm.output_file = "src/CommonAssemblyInfo.cs"
  	asm.version = "#{BUILD_VERSION}"
	end

	task :clean do
		rm_rf "#{BUILD_PATH}"
		rm_rf "#{ARTIFACTS_PATH}"
		rm_rf "#{REPORTS_PATH}"
		rm_rf "#{REPORTS_PATH}"
		rm_rf "#{LIB_PATH}"
	end

	task :compile do
		mkdir "#{BUILD_PATH}"
		sh "#{MSBUILD_PATH}msbuild.exe /p:Configuration=#{COMPILE_TARGET} #{SOLUTION}"
		copyOutputFiles "src/ExpectedObjects/bin/#{COMPILE_TARGET}", "*.{dll,pdb}", "#{BUILD_PATH}"
	end

	task :tests do
		mkdir_p "#{REPORTS_PATH}"
		specs = FileList.new("src/ExpectedObjects.Specs/bin/#{COMPILE_TARGET}/*.Specs.dll")
		sh "lib/Machine.Specifications.0.4.9.0/tools/mspec-x86-clr4.exe -x integration #{specs}"
	end

	task :package do
		mkdir_p "#{ARTIFACTS_PATH}"
		rm Dir.glob("#{ARTIFACTS_PATH}/*.nupkg")
		FileList["packaging/nuget/*.nuspec"].each do |spec|
		sh "#{nuget} pack #{spec} -o #{ARTIFACTS_PATH} -Version #{BUILD_VERSION} -Symbols"
	end
  end
	def copyOutputFiles(fromDir, filePattern, outDir)
		Dir.glob(File.join(fromDir, filePattern)){|file| 		
			copy(file, outDir) if File.file?(file)
  		} 
	end
end
