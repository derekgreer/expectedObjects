require 'rubygems'
require 'albacore'

MSBUILD_PATH = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/"
BUILD_PATH = File.expand_path('build')
REPORTS_PATH = File.expand_path('reports')
ARTIFACTS_PATH = File.expand_path('artifacts')
SOLUTION = "src/ExpectedObjects.sln"
COMPILE_TARGET = "Release"
nuget = 'nuget'
load "VERSION.txt"

task :default => ["all"]

task :all => [:clean, :compile, :specs, :package]

assemblyinfo :versioning do |asm|
	asm.output_file = "src/CommonAssemblyInfo.cs"
 	asm.version = "#{BUILD_VERSION}"
end

task :clean do
	rm_rf "#{BUILD_PATH}"
	rm_rf "#{ARTIFACTS_PATH}"
	rm_rf "#{REPORTS_PATH}"
end

task :compile => [:versioning] do

	mkdir "#{BUILD_PATH}"
	sh "#{MSBUILD_PATH}msbuild.exe /p:Configuration=#{COMPILE_TARGET} #{SOLUTION}"
	copyOutputFiles "src/ExpectedObjects/bin/#{COMPILE_TARGET}", "*.{dll,pdb}", "#{BUILD_PATH}"
end

task :specs do
	mkdir_p "#{REPORTS_PATH}"
	specs = FileList.new("src/ExpectedObjects.Specs/bin/#{COMPILE_TARGET}/*.Specs.dll")
	sh "src/packages/Machine.Specifications.0.8.3/tools/mspec-x86-clr4.exe -x integration #{specs}"
end

task :package do
	mkdir_p "#{ARTIFACTS_PATH}"
	rm Dir.glob("#{ARTIFACTS_PATH}/*.nupkg")
	FileList["packaging/nuget/*.nuspec"].each do |spec|
		sh "#{nuget} pack #{spec} -o #{ARTIFACTS_PATH} -Version #{BUILD_VERSION} -Symbols -BasePath ."
	end
end

task :publish => [:all] do
        FileList["#{ARTIFACTS_PATH}/*.nupkg"].gsub(File::SEPARATOR,
     File::ALT_SEPARATOR || File::SEPARATOR).each do | file |
                sh "nuget push #{file}"
        end
end

def copyOutputFiles(fromDir, filePattern, outDir)
	Dir.glob(File.join(fromDir, filePattern)){|file| 		
		copy(file, outDir) if File.file?(file)
  	} 
end
