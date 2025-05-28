using Godot;
using System;

public partial class PlayerOneMovement : RigidBody2D
{
	// ΜΕΤΑΒΛΗΤΗ ΕΛΕΓΧΟΥ ΤΑΧΥΤΗΤΑΣ ΠΑΙΚΤΗ1, ΜΠΟΡΟΥΜΕ ΝΑ ΤΗ ΡΥΘΜΙΣΟΥΜΕ ΜΕΣΩ ENGINE ΛΟΓΩ 
	// Εxport KEYWORD
	
	[Export] public float movementSpeed = 5000f;
	
	// ΜΕΤΑΒΛΗΤΗ ΓΙΑ ΤΗΝ ΤΕΛΕΥΤΑΙΑ ΚΑΤΕΥΘΥΝΣΗ
	
	private Vector2 lastDirection = Vector2.Right; // ΑΡΧΙΚΗ ΚΑΤΕΥΘΥΝΣΗ (ΔΕΞΙΑ ΓΙΑ PLAYER2)
	
	// ΑΡΧΙΚΟΠΟΙΗΣΗ ΣΤΗΝ READY
	
	public override void _Ready()
	{
		LockRotation = false;  // ΕΠΙΤΡΕΠΟΥΜΕ ΠΕΡΙΣΤΡΟΦΗ
		Rotation = lastDirection.Angle();  // ΑΡΧΙΚΗ ΠΕΡΙΣΤΡΟΦΗ
	}
	
	// KANOYME ΟVERRIDE ΤΑ PHYSICS ΤΟΥ ENGINE ΚΑΙ ΧΡΗΣΙΜΟΠΟΙΟΥΜΕ ΤΑ ΔΙΚΑ ΜΑΣ ΓΙΑ ΤΗ
	// ΚΙΝΗΣΗ ΤΟΥ ΠΑΙΚΤΗ 
	
	public override void _PhysicsProcess(double deltaTime)
	{
		// ΑΡΧΗΚΟΠΟΙΗΣΗ VECTOR2 ΩΣ 0,0 (ΔΕΝ ΥΠΑΡΧΕΙ ΚΙΝΗΣΗ)
		
		Vector2 direction = Vector2.Zero;
		
		// ΕΛΕΓΧΟΣ ΓΙΑ ΤΗ ΧΡΗΣΗ ΤΩΝ ΠΑΡΑΚΑΤΩ ΠΛΗΚΤΡΩΝ ΑΠΟ ΤΟΝ ΧΡΗΣΤΗ ΚΑΙ ΕΦΑΡΜΟΓΗ ΦΥΣΙΚΩΝ
		// ΔΥΝΑΜΕΩΝ ΣΤΟ RIGIDBODY ΑΝΑΛΟΓΩΣ
		
		if (Input.IsKeyPressed(Key.A)) direction.X -= 1;  // Left
		if (Input.IsKeyPressed(Key.D)) direction.X += 1;  // Right
		if (Input.IsKeyPressed(Key.W)) direction.Y -= 1;  // Up
		if (Input.IsKeyPressed(Key.S)) direction.Y += 1;  // Down
		
		if (direction.Length() > 0)
		{
			// ΚΑΙ ΚΑΛΑ ΓΙΑ ΝΑ ΠΑΡΑΜΕΝΕΙ ΣΤΑΘΕΡΗ Η ΤΑΧΥΤΗΤΑ IDK
			
			direction = direction.Normalized();
			
			// ΑΠΟΘΗΚΕΥΣΗ ΚΑΤΕΥΘΥΝΣΗΣ
			
			lastDirection = direction;
		}
		
		// ΓΙΑ ΚΟΙΤΑΖΕΙ ΠΡΟΣ ΤΗΝ ΤΕΛΕΥΤΑΙΑ ΚΑΤΕΥΘΥΝΣΗ ΣΥΝΕΧΩΣ
		
		Rotation = lastDirection.Angle();
		
		// ΤΕΛΙΚΗ ΕΦΑΡΜΟΓΗ ΔΥΝΑΜΕΩΝ ΣΤΟ ΚΕΝΤΡΟ ΤΟΥ RIGIDBODY ΑΝΑΛΟΓΩΣ ΚΑΤΕΥΘΥΝΣΗΣ ΠΟΥ ΕΔΩΣΕ
		// Ο ΧΡΗΣΤΗΣ ΚΑΙ ΤΗΣ ΤΑΧΥΤΗΤΑΣ ΠΟΥ ΘΕΣΑΜΕ ΕΜΕΙΣ
		
		ApplyCentralForce(direction * movementSpeed);
	}
}

// ΣΗΜΕΙΩΣΗ ΑΠΟ ΝΙΚΟ. Ο ΠΑΙΚΤΗΣ1 ΘΑ ΧΡΗΣΙΜΟΠΟΙΕΙ WASD ΓΙΑ ΝΑ ΚΙΝΕΙΤΑΙ ΟΠΩΣ ΦΑΙΝΕΤΑΙ ΣΤΑ
// IF STATEMENTS ΠΑΝΩ

// ΣΗΜΕΙΩΣΗ 2 ΑΠΟ ΝΙΚΟ. ΕΚΑΝΑ ΠΡΟΣΩΡΙΝΑ LOCK ROTATION KAI ΑΥΞΗΣΑ ΤΟ LINEAR DAMP ΣΤΟ 5
// ΓΙΑ ΝΑ ΕΧΕΙ ΠΙΟ 'SHARP' (haha get it?) FEELING TO MOVEMENT
