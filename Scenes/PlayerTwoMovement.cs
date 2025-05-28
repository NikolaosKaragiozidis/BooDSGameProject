using Godot;
using System;

public partial class PlayerTwoMovement : RigidBody2D
{
	// ΜΕΤΑΒΛΗΤΗ ΕΛΕΓΧΟΥ ΤΑΧΥΤΗΤΑΣ ΠΑΙΚΤΗ1, ΜΠΟΡΟΥΜΕ ΝΑ ΤΗ ΡΥΘΜΙΣΟΥΜΕ ΜΕΣΩ ENGINE ΛΟΓΩ 
	// Εxport KEYWORD
	
	[Export] public float movementSpeed = 5000f;
	
	// ΜΕΤΑΒΛΗΤΗ ΓΙΑ ΤΗΝ ΤΕΛΕΥΤΑΙΑ ΚΑΤΕΥΘΥΝΣΗ
	
	private Vector2 lastDirection = Vector2.Left;  // ΑΡΧΙΚΗ ΚΑΤΕΥΘΥΝΣΗ (ΑΡΙΣΤΕΡΑ ΓΙΑ PLAYER2)
	
	// ΑΡΧΙΚΟΠΟΙΗΣΗ ΣΤΗΝ READY
	
	public override void _Ready()
	{
		LockRotation = false; // ΕΠΙΤΡΕΠΟΥΜΕ ΠΕΡΙΣΤΡΟΦΗ
		
		// ΠΡΟΣΘΗΚΗ 90 ΜΟΙΡΩΝ OFFSET ΓΙΑ ΤΟ ΣΠΡΑΙΤ2D (ΓΑΜΩ)
		Rotation = lastDirection.Angle() + Mathf.DegToRad(-180);
	}
	
	// KANOYME ΟVERRIDE ΤΑ PHYSICS ΤΟΥ ENGINE ΚΑΙ ΧΡΗΣΙΜΟΠΟΙΟΥΜΕ ΤΑ ΔΙΚΑ ΜΑΣ ΓΙΑ ΤΗ
	// ΚΙΝΗΣΗ ΤΟΥ ΠΑΙΚΤΗ
	
	public override void _PhysicsProcess(double deltaTime)
	{
		// ΑΡΧΗΚΟΠΟΙΗΣΗ VECTOR2 ΩΣ 0,0 (ΔΕΝ ΥΠΑΡΧΕΙ ΚΙΝΗΣΗ)
		
		Vector2 direction = Vector2.Zero;
		
		// ΕΛΕΓΧΟΣ ΓΙΑ ΤΗ ΧΡΗΣΗ ΤΩΝ ΠΑΡΑΚΑΤΩ ΠΛΗΚΤΡΩΝ ΑΠΟ ΤΟΝ ΧΡΗΣΤΗ ΚΑΙ ΕΦΑΡΜΟΓΗ ΦΥΣΙΚΩΝ
		// ΔΥΝΑΜΕΩΝ ΣΤΟ RIGIDBODY ΑΝΑΛΟΓΩΣ
		
		if (Input.IsKeyPressed(Key.Left)) direction.X -= 1;
		if (Input.IsKeyPressed(Key.Right)) direction.X += 1;
		if (Input.IsKeyPressed(Key.Up)) direction.Y -= 1;
		if (Input.IsKeyPressed(Key.Down)) direction.Y += 1;
		
		if (direction.Length() > 0)
		{
			// ΚΑΙ ΚΑΛΑ ΓΙΑ ΝΑ ΠΑΡΑΜΕΝΕΙ ΣΤΑΘΕΡΗ Η ΤΑΧΥΤΗΤΑ IDK
			
			direction = direction.Normalized();
			
			// ΑΠΟΘΗΚΕΥΣΗ ΚΑΤΕΥΘΥΝΣΗΣ
			
			lastDirection = direction;
		}
		
		// ΓΑΜΩ ΤΑ OFFSET ΜΟΥ ΓΑΜΩ ΜΟΥ ΠΗΡΕ 40 ΛΕΠΤΑ ΝΑ ΚΑΤΑΛΑΒΩ ΤΙ ΓΙΝΕΤΑΙ
		
		Rotation = lastDirection.Angle() + Mathf.DegToRad(-180); // ΓΙΑ ΚΟΙΤΑΖΕΙ ΠΡΟΣ ΤΗΝ ΤΕΛΕΥΤΑΙΑ ΚΑΤΕΥΘΥΝΣΗ ΣΥΝΕΧΩΣ
		
		// ΤΕΛΙΚΗ ΕΦΑΡΜΟΓΗ ΔΥΝΑΜΕΩΝ ΣΤΟ ΚΕΝΤΡΟ ΤΟΥ RIGIDBODY ΑΝΑΛΟΓΩΣ ΚΑΤΕΥΘΥΝΣΗΣ ΠΟΥ ΕΔΩΣΕ
		// Ο ΧΡΗΣΤΗΣ ΚΑΙ ΤΗΣ ΤΑΧΥΤΗΤΑΣ ΠΟΥ ΘΕΣΑΜΕ ΕΜΕΙΣ
		
		ApplyCentralForce(direction * movementSpeed);
	}
}

// ΣΗΜΕΙΩΣΗ ΑΠΟ ΝΙΚΟ. Ο ΠΑΙΚΤΗΣ2 ΘΑ ΧΡΗΣΙΜΟΠΟΙΕΙ ARROW ΓΙΑ ΝΑ ΚΙΝΕΙΤΑΙ ΟΠΩΣ ΦΑΙΝΕΤΑΙ 
// ΣΤΑ IF STATEMENTS ΠΑΝΩ

// ΣΗΜΕΙΩΣΗ 2 ΑΠΟ ΝΙΚΟ. ΕΚΑΝΑ ΠΡΟΣΩΡΙΝΑ LOCK ROTATION KAI ΑΥΞΗΣΑ ΤΟ LINEAR DAMP ΣΤΟ 5
// ΓΙΑ ΝΑ ΕΧΕΙ ΠΙΟ 'SHARP' (haha get it?) FEELING TO MOVEMENT
