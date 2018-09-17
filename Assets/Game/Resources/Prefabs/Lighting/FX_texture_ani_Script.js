	// Change renderer's texture each changeInterval/
	// seconds from the texture array defined in the inspector.	
	
	var textures : Texture[];
	var changeInterval : float = 0.05;
	
	function Update() {
		if( textures.length == 0 ) // nothing if no textures
			return;		// we want this texture index now
		var index : int = Time.time / changeInterval;
		// take a modulo with size so that animation repeats
		index = index % textures.length;
		// assign it
		GetComponent.<Renderer>().material.mainTexture = textures[index];
	}
