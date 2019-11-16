# Hololens Experimenting

A place where I experiment with Hololens Development

## About

This project repo is a playground where I experiment with developing apps for the Hololens 2.  If you visit this repo, keep a couple of things in mind:
1. I am still fairly unfamiliar with managing project repos in GitHub.  I have primarily (if not, exclusively) used Azure DevOps (formerly Team Services).  Please forgive me while I get familiar with the tools.
2. I am most familiar with building traditional apps (desktop and web), and, even though I work as an application developer, it is in an organization where I am the primary develoepr and have one junior developer that had no prior software development experience.  With that in mind, I may take certain approaches that are unconventional/unorthodox because I do not have the experience of working with larger teams.

I guess one other thing to keep in mind is: while I have dabbled with Unity in the past, and followed many tutorials, I still have not created a full project using it.  It is another toolset that I am mostly unfamiliar with its capabilities, methodologies, and overall use.

Regardless, I hope to learn a lot about GitHub, developing in Unity, and my ultimate goal is to learn how to develop with the aim of deploying to multiple platforms, including XR.

## Progress So Far

I will use this section to list things that I have done with regards to this repo.  Hopefully it will help me track things I have tried, how they turned out, and what I have learned from them.

### Entry 2019-11-16.1:

Created GitHub project page for a space to experiment with Hololen Development.  While typing this, I realized that this really should be for XR, but I plan on targetting the Hololens 2 specifically, so hopefully it was a good choice to name the project this.

### Entry: 2019-11-16.2:

Came up with a format for entering information into this section.  Weird that I am logging an entry about logging entries, but it is helping me feel a bit more loose with this process.  Hopefully it will keep me feeling relaxed while experimenting in a public space.

## Entry: 2019-11-16.3:

I am very used to creating a new project in Visual Studio and using the IDE to add the solution to a source control repo on Azure DevOps.  I figured this time I would try to use the command line approach to using GitHub (instead of the Git tools in Visual Studio).  To begin with, I opened up powershell (since I am using Windows) and navigated to the folder I want to use for the local copy of this repo.  I was pretty sure I had already downloaded Git and got everything setup some time ago, so I followed the steps I somewhat remembered from my first encounter with the process (what do you know, I am also using this to learn a bit more about markdown since I only know what I have used for wiki pages in Azure DevOps repos):
  `git clone {this_repos_git_clone.git}`
Next, I created a new file to add (wth the thought that my next step would be to learn how to add it to the git ignore, followed by learning how to delete it from the repo).  After this, I googled how to check in changes with a new file.  I found https://githowto.com/pushing_a_change to get a feel for how to add files to the repo.  It showed me how to commit changes using a shared branch, which I had no idea what it meant, so instead I ran:
```
git pull origin master #which did nothing since everything is already up to date 
git checkout master #which did nothing since I am already on the master branch
git add .\LearningGit.txt #the new file that I added
git commit -m "Trying a push for the first time"
git push
```
It wasn't until I started typing this that I realized, I should probably have started at the beginning of the resource I found.  So, I'm going to read through that tutorial from the top.
