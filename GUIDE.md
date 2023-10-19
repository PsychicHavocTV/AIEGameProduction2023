# GIT GUIDE
## [Cloning the Repository](https://www.atlassian.com/git/tutorials/setting-up-a-repository/git-clone)
### - [GUI] Frontend Client
This is the **easiest** and **recommended** method.\
You can use a **frontend client** such as [SourceTree](https://www.sourcetreeapp.com/) or [Github Desktop](https://desktop.github.com/) among many others.\
**SourceTree** is the recommended choice and this README will be written in reference to it.

Once installed, you will want to sign into your Github account using the interface, then click the button labeled **"Clone"**,\
from there you want to input the address of the repository: `https://github.com/PsychicHavocTV/AIEGameProduction2022.git`.\
Afterwards you can input the directory where the repository will be cloned to, this should be an empty folder.

### - [CLI] Command Line
If for some reason you cannot use a frontend client, you can instead clone the project using the **command line**.\
You want to make sure you have [Git](https://git-scm.com/) installed and setup correctly, then from the command line you want to navigate to the directory you want to clone the repository to, again this should be an empty folder. Then you can use the command:
```bat
git clone --recursive https://github.com/PsychicHavocTV/AIEGameProduction2022.git
```
If you have a frontend client already installed, **Git** should already be installed and setup.

## Pushing and Pulling
**[Pushing](https://www.atlassian.com/git/tutorials/syncing/git-push) and [Pulling](https://www.atlassian.com/git/tutorials/syncing/git-pull)** is a concept in Git that allows you to add files to a **repository**, update them, or download them.

### - Pushing
When **[pushing](https://www.atlassian.com/git/tutorials/syncing/git-push)** files to a repository you first want to stage any new or modified files to a **[commit](https://www.atlassian.com/git/tutorials/saving-changes/git-commit)**.

This is easy enough using a **frontend client** as any new changes should appear within the interface with a button allowing you to **stage** them, afterwards you can click a button labeled **"Commit"** and input a **commit message**. The **commit message** is required and very recommended.

After you've **commited** your changes, you can finally **push** the **commit** to the repository.\
This is also easy using the Frontend Client as there should be a button labeled **"Push"**. Please make sure you're **pushing** your changes to the right **branch** (More info below).

For the **command line** it's a similar process:\
To stage specific files use the command:
```bat
git add [File Name]
```
Though not recommended, you can also use this command to **stage** all modified files:
```bat
git add .
```
To see which files are currently **staged** use:
```bat
git status
```
To commit your **staged** changes use this command, do not forget the quotes:
```bat
git commit -m "[Commit Message]"
```
Then to finally **push** the **commit** to the repository use:
```bat
git push origin [Branch Name]
```

### - Pulling
**[Pulling](https://www.atlassian.com/git/tutorials/syncing/git-pull)** is an easier process for the most part, it allows you to recieve any new changes made to the repository.

The first step is to make sure you are in the right **branch** (More info below).\
Then, you need to make sure that you have no current changes since the last commit that could cause any **[conflicts](https://www.atlassian.com/git/tutorials/using-branches/merge-conflicts)** with the new changes.\
If you do, you will have to either **[discard](https://www.atlassian.com/git/tutorials/undoing-changes/git-reset)** those changes, **[stash](https://www.atlassian.com/git/tutorials/saving-changes/git-stash)** them, or try to **[resolve](https://www.atlassian.com/git/tutorials/using-branches/merge-conflicts)** the conflicts after pulling.

In the **frontend client** it is then simple as just clicking the button labeled **"Pull"**.

For the **command line** the process is simply:
```bat
git pull origin [Branch Name]
```

## [Branches](https://git-scm.com/book/en/v2/Git-Branching-Branches-in-a-Nutshell)
### - Using Branches
When you first **clone** a repository the **branch** will be defaulted to the **Main** branch.\
To switch to another branch you can **[checkout](https://www.atlassian.com/git/tutorials/using-branches/git-checkout)** a different **branch**.

If you're using a **frontend client** you should be able to simply right click and **checkout** a **branch**,\
If the **branch** you want to checkout doesn't seem to appear within the frontend client, you can use the client's interface to create a new **local** branch and then tell it to track the corresponding **remote** branch.

In the **command line** this looks like:
```bat
git checkout [Branch Name] 
```
You should then **pull** from the current branch.

Also, in the command line to find out which branch you're currently in:
```bat
git branch
```
