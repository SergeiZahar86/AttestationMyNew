/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
package com.indas.att.thrift;

@SuppressWarnings({"cast", "rawtypes", "serial", "unchecked", "unused"})
@javax.annotation.Generated(value = "Autogenerated by Thrift Compiler (0.13.0)", date = "2020-10-12")
public class Photo implements org.apache.thrift.TBase<Photo, Photo._Fields>, java.io.Serializable, Cloneable, Comparable<Photo> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("Photo");

  private static final org.apache.thrift.protocol.TField LEFT_FIELD_DESC = new org.apache.thrift.protocol.TField("left", org.apache.thrift.protocol.TType.STRING, (short)1);
  private static final org.apache.thrift.protocol.TField RIGHT_FIELD_DESC = new org.apache.thrift.protocol.TField("right", org.apache.thrift.protocol.TType.STRING, (short)2);
  private static final org.apache.thrift.protocol.TField TOP_FIELD_DESC = new org.apache.thrift.protocol.TField("top", org.apache.thrift.protocol.TType.STRING, (short)3);

  private static final org.apache.thrift.scheme.SchemeFactory STANDARD_SCHEME_FACTORY = new PhotoStandardSchemeFactory();
  private static final org.apache.thrift.scheme.SchemeFactory TUPLE_SCHEME_FACTORY = new PhotoTupleSchemeFactory();

  public @org.apache.thrift.annotation.Nullable java.nio.ByteBuffer left; // required
  public @org.apache.thrift.annotation.Nullable java.nio.ByteBuffer right; // required
  public @org.apache.thrift.annotation.Nullable java.nio.ByteBuffer top; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    LEFT((short)1, "left"),
    RIGHT((short)2, "right"),
    TOP((short)3, "top");

    private static final java.util.Map<java.lang.String, _Fields> byName = new java.util.HashMap<java.lang.String, _Fields>();

    static {
      for (_Fields field : java.util.EnumSet.allOf(_Fields.class)) {
        byName.put(field.getFieldName(), field);
      }
    }

    /**
     * Find the _Fields constant that matches fieldId, or null if its not found.
     */
    @org.apache.thrift.annotation.Nullable
    public static _Fields findByThriftId(int fieldId) {
      switch(fieldId) {
        case 1: // LEFT
          return LEFT;
        case 2: // RIGHT
          return RIGHT;
        case 3: // TOP
          return TOP;
        default:
          return null;
      }
    }

    /**
     * Find the _Fields constant that matches fieldId, throwing an exception
     * if it is not found.
     */
    public static _Fields findByThriftIdOrThrow(int fieldId) {
      _Fields fields = findByThriftId(fieldId);
      if (fields == null) throw new java.lang.IllegalArgumentException("Field " + fieldId + " doesn't exist!");
      return fields;
    }

    /**
     * Find the _Fields constant that matches name, or null if its not found.
     */
    @org.apache.thrift.annotation.Nullable
    public static _Fields findByName(java.lang.String name) {
      return byName.get(name);
    }

    private final short _thriftId;
    private final java.lang.String _fieldName;

    _Fields(short thriftId, java.lang.String fieldName) {
      _thriftId = thriftId;
      _fieldName = fieldName;
    }

    public short getThriftFieldId() {
      return _thriftId;
    }

    public java.lang.String getFieldName() {
      return _fieldName;
    }
  }

  // isset id assignments
  public static final java.util.Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    java.util.Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new java.util.EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.LEFT, new org.apache.thrift.meta_data.FieldMetaData("left", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING        , true)));
    tmpMap.put(_Fields.RIGHT, new org.apache.thrift.meta_data.FieldMetaData("right", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING        , true)));
    tmpMap.put(_Fields.TOP, new org.apache.thrift.meta_data.FieldMetaData("top", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING        , true)));
    metaDataMap = java.util.Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(Photo.class, metaDataMap);
  }

  public Photo() {
  }

  public Photo(
    java.nio.ByteBuffer left,
    java.nio.ByteBuffer right,
    java.nio.ByteBuffer top)
  {
    this();
    this.left = org.apache.thrift.TBaseHelper.copyBinary(left);
    this.right = org.apache.thrift.TBaseHelper.copyBinary(right);
    this.top = org.apache.thrift.TBaseHelper.copyBinary(top);
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public Photo(Photo other) {
    if (other.isSetLeft()) {
      this.left = org.apache.thrift.TBaseHelper.copyBinary(other.left);
    }
    if (other.isSetRight()) {
      this.right = org.apache.thrift.TBaseHelper.copyBinary(other.right);
    }
    if (other.isSetTop()) {
      this.top = org.apache.thrift.TBaseHelper.copyBinary(other.top);
    }
  }

  public Photo deepCopy() {
    return new Photo(this);
  }

  @Override
  public void clear() {
    this.left = null;
    this.right = null;
    this.top = null;
  }

  public byte[] getLeft() {
    setLeft(org.apache.thrift.TBaseHelper.rightSize(left));
    return left == null ? null : left.array();
  }

  public java.nio.ByteBuffer bufferForLeft() {
    return org.apache.thrift.TBaseHelper.copyBinary(left);
  }

  public Photo setLeft(byte[] left) {
    this.left = left == null ? (java.nio.ByteBuffer)null   : java.nio.ByteBuffer.wrap(left.clone());
    return this;
  }

  public Photo setLeft(@org.apache.thrift.annotation.Nullable java.nio.ByteBuffer left) {
    this.left = org.apache.thrift.TBaseHelper.copyBinary(left);
    return this;
  }

  public void unsetLeft() {
    this.left = null;
  }

  /** Returns true if field left is set (has been assigned a value) and false otherwise */
  public boolean isSetLeft() {
    return this.left != null;
  }

  public void setLeftIsSet(boolean value) {
    if (!value) {
      this.left = null;
    }
  }

  public byte[] getRight() {
    setRight(org.apache.thrift.TBaseHelper.rightSize(right));
    return right == null ? null : right.array();
  }

  public java.nio.ByteBuffer bufferForRight() {
    return org.apache.thrift.TBaseHelper.copyBinary(right);
  }

  public Photo setRight(byte[] right) {
    this.right = right == null ? (java.nio.ByteBuffer)null   : java.nio.ByteBuffer.wrap(right.clone());
    return this;
  }

  public Photo setRight(@org.apache.thrift.annotation.Nullable java.nio.ByteBuffer right) {
    this.right = org.apache.thrift.TBaseHelper.copyBinary(right);
    return this;
  }

  public void unsetRight() {
    this.right = null;
  }

  /** Returns true if field right is set (has been assigned a value) and false otherwise */
  public boolean isSetRight() {
    return this.right != null;
  }

  public void setRightIsSet(boolean value) {
    if (!value) {
      this.right = null;
    }
  }

  public byte[] getTop() {
    setTop(org.apache.thrift.TBaseHelper.rightSize(top));
    return top == null ? null : top.array();
  }

  public java.nio.ByteBuffer bufferForTop() {
    return org.apache.thrift.TBaseHelper.copyBinary(top);
  }

  public Photo setTop(byte[] top) {
    this.top = top == null ? (java.nio.ByteBuffer)null   : java.nio.ByteBuffer.wrap(top.clone());
    return this;
  }

  public Photo setTop(@org.apache.thrift.annotation.Nullable java.nio.ByteBuffer top) {
    this.top = org.apache.thrift.TBaseHelper.copyBinary(top);
    return this;
  }

  public void unsetTop() {
    this.top = null;
  }

  /** Returns true if field top is set (has been assigned a value) and false otherwise */
  public boolean isSetTop() {
    return this.top != null;
  }

  public void setTopIsSet(boolean value) {
    if (!value) {
      this.top = null;
    }
  }

  public void setFieldValue(_Fields field, @org.apache.thrift.annotation.Nullable java.lang.Object value) {
    switch (field) {
    case LEFT:
      if (value == null) {
        unsetLeft();
      } else {
        if (value instanceof byte[]) {
          setLeft((byte[])value);
        } else {
          setLeft((java.nio.ByteBuffer)value);
        }
      }
      break;

    case RIGHT:
      if (value == null) {
        unsetRight();
      } else {
        if (value instanceof byte[]) {
          setRight((byte[])value);
        } else {
          setRight((java.nio.ByteBuffer)value);
        }
      }
      break;

    case TOP:
      if (value == null) {
        unsetTop();
      } else {
        if (value instanceof byte[]) {
          setTop((byte[])value);
        } else {
          setTop((java.nio.ByteBuffer)value);
        }
      }
      break;

    }
  }

  @org.apache.thrift.annotation.Nullable
  public java.lang.Object getFieldValue(_Fields field) {
    switch (field) {
    case LEFT:
      return getLeft();

    case RIGHT:
      return getRight();

    case TOP:
      return getTop();

    }
    throw new java.lang.IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new java.lang.IllegalArgumentException();
    }

    switch (field) {
    case LEFT:
      return isSetLeft();
    case RIGHT:
      return isSetRight();
    case TOP:
      return isSetTop();
    }
    throw new java.lang.IllegalStateException();
  }

  @Override
  public boolean equals(java.lang.Object that) {
    if (that == null)
      return false;
    if (that instanceof Photo)
      return this.equals((Photo)that);
    return false;
  }

  public boolean equals(Photo that) {
    if (that == null)
      return false;
    if (this == that)
      return true;

    boolean this_present_left = true && this.isSetLeft();
    boolean that_present_left = true && that.isSetLeft();
    if (this_present_left || that_present_left) {
      if (!(this_present_left && that_present_left))
        return false;
      if (!this.left.equals(that.left))
        return false;
    }

    boolean this_present_right = true && this.isSetRight();
    boolean that_present_right = true && that.isSetRight();
    if (this_present_right || that_present_right) {
      if (!(this_present_right && that_present_right))
        return false;
      if (!this.right.equals(that.right))
        return false;
    }

    boolean this_present_top = true && this.isSetTop();
    boolean that_present_top = true && that.isSetTop();
    if (this_present_top || that_present_top) {
      if (!(this_present_top && that_present_top))
        return false;
      if (!this.top.equals(that.top))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    int hashCode = 1;

    hashCode = hashCode * 8191 + ((isSetLeft()) ? 131071 : 524287);
    if (isSetLeft())
      hashCode = hashCode * 8191 + left.hashCode();

    hashCode = hashCode * 8191 + ((isSetRight()) ? 131071 : 524287);
    if (isSetRight())
      hashCode = hashCode * 8191 + right.hashCode();

    hashCode = hashCode * 8191 + ((isSetTop()) ? 131071 : 524287);
    if (isSetTop())
      hashCode = hashCode * 8191 + top.hashCode();

    return hashCode;
  }

  @Override
  public int compareTo(Photo other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = java.lang.Boolean.valueOf(isSetLeft()).compareTo(other.isSetLeft());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetLeft()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.left, other.left);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = java.lang.Boolean.valueOf(isSetRight()).compareTo(other.isSetRight());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetRight()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.right, other.right);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = java.lang.Boolean.valueOf(isSetTop()).compareTo(other.isSetTop());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetTop()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.top, other.top);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    return 0;
  }

  @org.apache.thrift.annotation.Nullable
  public _Fields fieldForId(int fieldId) {
    return _Fields.findByThriftId(fieldId);
  }

  public void read(org.apache.thrift.protocol.TProtocol iprot) throws org.apache.thrift.TException {
    scheme(iprot).read(iprot, this);
  }

  public void write(org.apache.thrift.protocol.TProtocol oprot) throws org.apache.thrift.TException {
    scheme(oprot).write(oprot, this);
  }

  @Override
  public java.lang.String toString() {
    java.lang.StringBuilder sb = new java.lang.StringBuilder("Photo(");
    boolean first = true;

    sb.append("left:");
    if (this.left == null) {
      sb.append("null");
    } else {
      org.apache.thrift.TBaseHelper.toString(this.left, sb);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("right:");
    if (this.right == null) {
      sb.append("null");
    } else {
      org.apache.thrift.TBaseHelper.toString(this.right, sb);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("top:");
    if (this.top == null) {
      sb.append("null");
    } else {
      org.apache.thrift.TBaseHelper.toString(this.top, sb);
    }
    first = false;
    sb.append(")");
    return sb.toString();
  }

  public void validate() throws org.apache.thrift.TException {
    // check for required fields
    // check for sub-struct validity
  }

  private void writeObject(java.io.ObjectOutputStream out) throws java.io.IOException {
    try {
      write(new org.apache.thrift.protocol.TCompactProtocol(new org.apache.thrift.transport.TIOStreamTransport(out)));
    } catch (org.apache.thrift.TException te) {
      throw new java.io.IOException(te);
    }
  }

  private void readObject(java.io.ObjectInputStream in) throws java.io.IOException, java.lang.ClassNotFoundException {
    try {
      read(new org.apache.thrift.protocol.TCompactProtocol(new org.apache.thrift.transport.TIOStreamTransport(in)));
    } catch (org.apache.thrift.TException te) {
      throw new java.io.IOException(te);
    }
  }

  private static class PhotoStandardSchemeFactory implements org.apache.thrift.scheme.SchemeFactory {
    public PhotoStandardScheme getScheme() {
      return new PhotoStandardScheme();
    }
  }

  private static class PhotoStandardScheme extends org.apache.thrift.scheme.StandardScheme<Photo> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, Photo struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // LEFT
            if (schemeField.type == org.apache.thrift.protocol.TType.STRING) {
              struct.left = iprot.readBinary();
              struct.setLeftIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 2: // RIGHT
            if (schemeField.type == org.apache.thrift.protocol.TType.STRING) {
              struct.right = iprot.readBinary();
              struct.setRightIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 3: // TOP
            if (schemeField.type == org.apache.thrift.protocol.TType.STRING) {
              struct.top = iprot.readBinary();
              struct.setTopIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          default:
            org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
        }
        iprot.readFieldEnd();
      }
      iprot.readStructEnd();

      // check for required fields of primitive type, which can't be checked in the validate method
      struct.validate();
    }

    public void write(org.apache.thrift.protocol.TProtocol oprot, Photo struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      if (struct.left != null) {
        oprot.writeFieldBegin(LEFT_FIELD_DESC);
        oprot.writeBinary(struct.left);
        oprot.writeFieldEnd();
      }
      if (struct.right != null) {
        oprot.writeFieldBegin(RIGHT_FIELD_DESC);
        oprot.writeBinary(struct.right);
        oprot.writeFieldEnd();
      }
      if (struct.top != null) {
        oprot.writeFieldBegin(TOP_FIELD_DESC);
        oprot.writeBinary(struct.top);
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class PhotoTupleSchemeFactory implements org.apache.thrift.scheme.SchemeFactory {
    public PhotoTupleScheme getScheme() {
      return new PhotoTupleScheme();
    }
  }

  private static class PhotoTupleScheme extends org.apache.thrift.scheme.TupleScheme<Photo> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, Photo struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TTupleProtocol oprot = (org.apache.thrift.protocol.TTupleProtocol) prot;
      java.util.BitSet optionals = new java.util.BitSet();
      if (struct.isSetLeft()) {
        optionals.set(0);
      }
      if (struct.isSetRight()) {
        optionals.set(1);
      }
      if (struct.isSetTop()) {
        optionals.set(2);
      }
      oprot.writeBitSet(optionals, 3);
      if (struct.isSetLeft()) {
        oprot.writeBinary(struct.left);
      }
      if (struct.isSetRight()) {
        oprot.writeBinary(struct.right);
      }
      if (struct.isSetTop()) {
        oprot.writeBinary(struct.top);
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, Photo struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TTupleProtocol iprot = (org.apache.thrift.protocol.TTupleProtocol) prot;
      java.util.BitSet incoming = iprot.readBitSet(3);
      if (incoming.get(0)) {
        struct.left = iprot.readBinary();
        struct.setLeftIsSet(true);
      }
      if (incoming.get(1)) {
        struct.right = iprot.readBinary();
        struct.setRightIsSet(true);
      }
      if (incoming.get(2)) {
        struct.top = iprot.readBinary();
        struct.setTopIsSet(true);
      }
    }
  }

  private static <S extends org.apache.thrift.scheme.IScheme> S scheme(org.apache.thrift.protocol.TProtocol proto) {
    return (org.apache.thrift.scheme.StandardScheme.class.equals(proto.getScheme()) ? STANDARD_SCHEME_FACTORY : TUPLE_SCHEME_FACTORY).getScheme();
  }
}

