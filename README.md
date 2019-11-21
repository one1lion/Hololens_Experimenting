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

### Entry: 2019-11-16.3:

I am very used to creating a new project in Visual Studio and using the IDE to add the solution to a source control repo on Azure DevOps.  I figured this time I would try to use the command line approach to using GitHub (instead of the Git tools in Visual Studio).  To begin with, I opened up powershell (since I am using Windows) and navigated to the folder I want to use for the local copy of this repo.  I was pretty sure I had already downloaded Git and got everything setup some time ago, so I followed the steps I somewhat remembered from my first encounter with the process (what do you know, I am also using this to learn a bit more about markdown since I only know what I have used for wiki pages in Azure DevOps repos):
  `git clone {this_repos_git_clone.git}`
Next, I created a new file to add (wth the thought that my next step would be to learn how to add it to the git ignore, followed by learning how to delete it from the repo).  After this, I googled how to check in changes with a new file.  I found https://githowto.com/pushing_a_change to get a feel for how to add files to the repo.  It showed me how to commit changes using a shared branch, which I had no idea what it meant, so instead I ran:
```powershell
git pull origin master #which did nothing since everything is already up to date 
git checkout master #which did nothing since I am already on the master branch
git add .\LearningGit.txt #the new file that I added
git commit -m "Trying a push for the first time"
git push
```
It wasn't until I started typing this that I realized, I should probably have started at the beginning of the resource I found.  So, I'm going to read through that tutorial from the top.

### Entry: 2019-11-16.4

I made it to 24 in the githowto walkthrough and figured I would create a branch to use for the [MR Basics Tutorial](https://docs.microsoft.com/en-us/windows/mixed-reality/holograms-100 "Microsoft Docs: MR Basics 100: Getting started with Unity").  I plan on creating the Unity project there and hopefully making checkpoint branches as I complete each major section.  I seem to do a lot of hoping at the end of each of these entries.  This is because I am not sure how much time I will be able to spend on this between working on projects for my job and making sure to spend quality time with my wife.  We'll see how this goes. 

### Entry: 2019-11-16.5

Well, that was an adventure. I am not sure if I have the correct .gitignore file for this, but it seems like I was able to get the project uploaded.  I am wondering if I should allow the entire project to get uploaded (as in, have a less restrictive .gitignore file) or not.  I'll have to do some research on that.  For now, I at least practiced a little with git add, mv, rm, branching, pulling, committing, and pull requests.  That'll require more tutorial-ing, too.  I think I am stopping for the night, though.  

### Entry: 2019-11-17.1

I still have a lot to learn, and hope to continue learning. For now, I have completed the tutorial through the 3rd page.  After checking it in to the first-tutorial branch, I created a pull request to merge it into master.  I have seen that there can be automated tests that could be put in place at each stage (pushing to the branch, then merging with master), and am sure I will get this setup when the time comes.  Just wanted to leave a note to remond myself.

### Entry: 2019-11-17.2

I completed through lesson 5, but couldn't quite get the touch sound to work on the Octa object.  It feels like I'm missing a step.  I added an Audio clip, but no audio file to it.  THe rest of the instructions were easy to follow, but I'm confused on this one.  I'll figure it out tomorrow.

### Entry: 2019-11-18.1

As expected, this is getting a lot easier.  At the same time, I feel like I am barely scratching the surface, both of using Git as the version/source control for Unity projects and Hololens 2 development in general.  I completed the Getting Started tutorial through lesson 7 and kind of want to start over with a new project folder, if not a full repo by following a tutorial on creating a Git repo for Unity projects.  I think I'll do that when I'm ready to do some actual work on one of the many projects I have in mind.  For now, I'm off to my Blazor projects.

### Entry: 2019-11-19.1

I did a little bit of research (watched one video) on using Git with a Unity project.  It looks like the original .gitignore file I used is pretty decent.  I'm going to continue the tutorials.  To do this, I plan on creating a new Unity project in the repo and trying to do the steps from first tutorial after previewing what the next tutorial is going to be like.

### Entry: 2019-11-19.2

I ran into a bit of snag while trying to go through the next module (Azure Spatial Anchors tutorials).  After performing all of the imports, I did not find the ButtonParent and ParentAnchor prefabs.  I spent some time looking around and could not get this resolved.  I also ran into a whole host of other issues, but finally got the second tutorial project back to a stable state and completed steps up through 6.  I am going to start on the one that I glazed over in the first paragraph.  It seems like the modules are not as accurate as they could be.  

### Entry: 2019-11-20.1

The previous tutorial on [Azure Spatial Anchors](https://docs.microsoft.com/en-us/windows/mixed-reality/mrlearning-asa-ch1) was pretty confusing to get through.  Not because the material was hard to understand, but because it was not very consistent.  There were download files at the start of the tutorial, as well as in-line with some of the steps.  It made it confusing to know which assets to import for the tutorial, but I eventually figured it out and was able to get through it.  Unfortunately, I still have not figured out how to get a project to build in a way that it works with the emulator.  

I started a new project from scratch (using Unity 2019.2.12f1) and imported the MRTK asset package.  Then, I applied the configurations to the scene and project.  I didn't add any other objects after this to make sure that wasn't a factor.  Then, I added the scene (after saving, of course) and built the solution in Unity.  I opened the solution file that was generated and followed the [steps for running a project in the Hololens emulator](https://docs.microsoft.com/en-us/windows/mixed-reality/using-the-hololens-emulator#deploying-apps-to-the-hololens-emulator).  When I run the project (either in Debug or Release, in x86 or x64, targeting the emulator, as well as publishing it to be able to side-load it using the device portal), it loads in the emulator, shows the four floating balls spinning about each other, then shows a debug error (in Visual Studio when the debugger is attached) of "Unhandled exception at 0x00007FFA5F0C971B (Windows.Mirage.dll) in {insert_app_name_here}.exe: Fatal program exit requested.".

I've tried the build in Unity using the MRTK Build Window as well as the regular File -> Build Settings window.  I've tried it in Release and Debug, with and without ticking the Development Build option, targeting Any Device or Hololens, and experience the same behavior.  There is limited information on the Interwebs (or I am not searching for the right thing).  It is really difficult to tell whether this is just because of the emulator, or if this will also happen when I get my hands on an actual device.  

For now, I am moving forward assuming that I am setting up the project correctly...but I would really like to test if the Hololens will work with Universal Render Pipeline (URP) (which I just finished creating a project using v2019.3.0b11).  I want to use it since Shader Graph does not work with the renderer that gets configured when using the MRTK's "Add to scene and configure" feature.  I followed [a tutorial on YouTube](https://youtu.be/taMp1g1pBeE) by Brackeys on the dissolve effect some time ago.  This made use of the Lightweight Render Pipeline (LWRP) and Shader Graph.  I immediately tried to use the same techniques with MRTK projects and believe I found resources pointing to the fact that you cannot use LWRP with Hololens projects.  

While doing research on how to use LWRP with the MRTK, I saw that Unity evolved LRP into the URP and figured I would give that a go.  It seems like everything worked, at least in the Unity player.  The project also built, but I am still getting the same results with the emulator.  One day I'll have a device to use.  

### Entry 2019-11-21.1

A little off-topic, but I wanted to add at least one entry for today.  I am deep into redesigning one of our Line of Business (LOB) applications and fear I will not be able to do much with this in the next few days or weeks.  I still plan on doing at least one AR-related thing each day, but I'll be putting in some overtime to try to get this crucial app to a much more performant state than it currently is.  

So, this morning, I played with ARCore a little bit.  I learned out how to setup a project for Android builds and added the AR Foundation, Subsystem, and ARCore packages.  I, then, added a cube to the scene and built/deployed to my android device.  It was pretty straight forward.  

On a slightly funny note, I remember when I was doing some android dev work in the past (around 2013), I played around with having a camera view (FrameLayout) with a draw space view (also a FrameLayout) that would show what the world-facing camera sees and allow you to draw android logos where you touched (or dragged after touching) on the screen.  My goal at the time was to learn how to use the camera to provide the background for a game of sorts.  A short while later, as phones and AI got better, apps were coming out that were able to recognize objects in images taken from the camera and made them interractable.  Markers were being used to place and overlay 2d and 3d objects onto scenes.  Now we have devices that beam light into your eyes, mixing holograms with what your eyes naturally see in the physical world.  I am super excited about this technology and hope that I have gained enough experience and knowledge to participate in shaping it.  

On a not-so-funny note, when I was going through the short tutorials on setting up projects for Android and iOS using ARCore and ARKit respectively, I was prompted to download and install the newest release of unity (2019.2.13f1).  I did so while I worked in .12f1.  After finishing the tutorials, I shut down Unity, and tried to launch a new project using .13f1.  It setup the project, installed the base packages and opened just fine.  I closed it and tried to use the new version for the ARCore tutorial project, and it looks like it tries to load, but then just goes back to the Unity Hub.  I tried to re-open the new Unity project, and it does the same (opens the Unity Hub without opening the project in Unity).  I, then, tried to open Unity directly (from start -> Unity, as well as from the install location (C:\Program Files\Unity\Hub\Editor\2019.2.13f1\Editor\Unity.exe). It just opens the Unity Hub.  There was a brief message at the bottom (took a while to get what it said, since it only appears for a fraction of a second) that reads "Unity is already in list. You cannot locate the same version".  I assume this means that Unity Hub is the default application for opening Unity...

After some research, it looked like a license issue, so I spent more time than I should trying to refresh, renew, return and active a new one.  I'm not sure at what point it finally worked, but I think it was because of this:

1 - Return License (it did not appear to do anything, but I assumed it did)
(bunch of other random stuff that probably didn't do anything)
2 - Force quit Unity Hub in Task Manager
3 - Reopen Unity Hub and see that there is no license
4 - Activate a new license
5 - See that the same license information is there from before activating a new one, except that it shows today as the Last Update

After that, everything worked as expected.  Glad I was able to figure it out, but not soemthing I really wanted to spend time on today.  Anyway, now that that's done, back to making the lives of a few administrative personnel better through automation.  
