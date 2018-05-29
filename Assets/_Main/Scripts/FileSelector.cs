using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class FileSelector {
	public abstract void Next ();
	public abstract void Prev ();
	public abstract FileInfo currentFile { get; }
	public abstract bool HasNext ();
	public abstract void Reset ();
}

public class SequencialSelector : FileSelector {
	List<FileInfo> files;
	int index = 0;

	public SequencialSelector (IEnumerable<FileInfo> collection) {
		files = new List<FileInfo> (collection);
	}

	public override void Next () {
		index = Mathf.Clamp (index + 1, 0, files.Count - 1);
	}
	public override void Prev () {
		index = Mathf.Clamp (index - 1, 0, files.Count - 1);
	}
	public override FileInfo currentFile {
		get {
			return files[index];
		}
	}
	public override bool HasNext () {
		return (index + 1) < files.Count;
	}

	public override void Reset () {
		index = 0;
	}
}

public class RandomSelector : FileSelector {
	IEnumerable<FileInfo> collection;
	List<FileInfo> files;
	FileInfo current = null;

	public RandomSelector (IEnumerable<FileInfo> collection) {
		this.collection = collection;
		files = new List<FileInfo>();
		Reset ();
	}

	public override void Next () {
		int index = Random.Range (0, files.Count);
		current = files[index];
		files.RemoveAt (index);
	}
	public override void Prev () { }
	public override FileInfo currentFile {
		get {
			return current;
		}
	}
	public override bool HasNext () {
		return files.Count > 0;
	}

	public override void Reset () {
		files.AddRange (collection);
		Next ();
	}
}